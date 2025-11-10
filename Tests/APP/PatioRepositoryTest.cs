using Microsoft.EntityFrameworkCore;
using Mongo2Go;
using MOTTHRU.API.Domain.Entities;
using MOTTHRU.API.Infrastructure.Data.AppData;
using MOTTHRU.API.Infrastructure.Data.Repository;

namespace Tests.APP;

public sealed class MongoInMemory : IDisposable  
{  
    public MongoDbRunner Runner { get; }  
  
    public MongoInMemory()  
    {  
        Runner = MongoDbRunner.Start(singleNodeReplSet: true, additionalMongodArguments: "--quiet");  
    }  
  
    public void Dispose()  
    {  
        Runner?.Dispose();  
    }  
}


public class PatioRepositoryTest : IClassFixture<MongoInMemory>
{
    private readonly MongoInMemory _mongoFixture;  
  
    public PatioRepositoryTest(MongoInMemory mongoFixture)  
    {  
        _mongoFixture = mongoFixture;  
    }
    
    private ApplicationContext CreateContext()  
    {  
        // Database único por teste evita colisão entre testes paralelos  
        var databaseName = $"db_test_{Guid.NewGuid():N}";  
        var connectionString = _mongoFixture.Runner.ConnectionString; 
  
        var options = new DbContextOptionsBuilder<ApplicationContext>()  
            .UseMongoDB(connectionString, databaseName)  
            .EnableSensitiveDataLogging()  
            .Options;  
  
        return new ApplicationContext(options);  
    }

    
    private static PatioEntity BuildPatio(  
        string? nomePatio = null )
    {  
        return new PatioEntity
        {  
            Id = 1,  
            NomePatio = nomePatio ?? "Patio Teste",
        };  
    }

    
    [Fact(DisplayName = "ObterTodosAsync deve paginar corretamente e retornar PageResultModel")]  
    [Trait("Repository", "Patios")]  
    public async Task ObterTodosAsync_DevePaginar()  
    {  
        // Arrange  
        using var ctx = CreateContext();  
        var repo = new PatioRepository(ctx);  
  
        // Seed de 4 patios  
        var c1 = BuildPatio("P1");  
        var c2 = BuildPatio("P2");  
        var c3 = BuildPatio("P3");  
        var c4 = BuildPatio("P4");  
  
        ctx.Patio.AddRange(c1, c2, c3, c4);  
        ctx.SaveChanges();  
  
        // Act  
        // Pega página com deslocamento 1 e tamanho 2 => espera C2 e C3 (ordenado por Id ASC)    var page = await repo.ObterTodosAsync(Deslocamento: 1, ResgistrosRetornado: 2);  
        var page = await repo.ObterTodosAsync();  
        
        // Assert  
        Assert.NotNull(page);  
        Assert.Equal(4, page.TotalRegistros);  
        Assert.Equal(1, page.Deslocamento);  
        Assert.Equal(2, page.RegistrosRetornado);  
        Assert.NotNull(page.Data);  
  
        var data = page.Data.ToList();  
        Assert.Equal(2, data.Count);  
    }

    [Fact(DisplayName = "ObterUmAsync deve retornar patio por Id")]  
    [Trait("Repository", "Patios")]  
    public async Task ObterUmAsync_DeveRetornarPatio()  
    {  
        using var ctx = CreateContext();  
        var repo = new PatioRepository(ctx);  
  
        var c1 = BuildPatio("P");  
        ctx.Patio.Add(c1);  
        ctx.SaveChanges();  
  
        var result = await repo.ObterUmAsync(c1.Id);  
        Assert.NotNull(result);  
        Assert.Equal("Maria", result!.NomePatio);  
    }

    [Fact(DisplayName = "AdicionarAsync deve inserir um patio e retornar a entidade")]  
    [Trait("Repository", "Patios")]  
    public async Task AdicionarAsync_DeveInserirPatioERetornar()  
    {  
        using var ctx = CreateContext();  
  
        var repo = new PatioRepository(ctx);  
  
        var novo = BuildPatio();  
  
        var salvo = await repo.AdicionarAsync(novo);  
  
        Assert.NotNull(salvo);  
        Assert.Equal(novo.Id, salvo!.Id);  
  
  
        // Confirma persistência  
        var reloaded = await ctx.Patio.FirstOrDefaultAsync(x => x.Id == novo.Id);  
        Assert.NotNull(reloaded);  
        Assert.Equal("Patio Teste", reloaded!.NomePatio);  
    }

    [Fact(DisplayName = "DeletarAsync deve remover patio e retornar a entidade removida")]  
    [Trait("Repository", "Patios")]  
    public async Task DeletarAsync_DeveRemoverPatio()  
    {  
        using var ctx = CreateContext();  
        var repo = new PatioRepository(ctx);  
  
        var patio = BuildPatio("P");  
        ctx.Patio.Add(patio);  
        ctx.SaveChanges();  
  
        var removido = await repo.DeletarAsync(patio.Id);  
        Assert.NotNull(removido);  
        Assert.Equal(patio.Id, removido!.Id);  
  
        var reloaded = await ctx.Patio.FindAsync(patio.Id);  
        Assert.Null(reloaded);  
    }

}