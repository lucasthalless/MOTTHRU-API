using Moq;
using MOTTHRU.API.Application.Dtos;
using MOTTHRU.API.Application.Mappers;
using MOTTHRU.API.Application.UseCases;
using MOTTHRU.API.Domain.Entities;
using MOTTHRU.API.Domain.Interfaces;

namespace Tests.APP;

public class PatioUseCaseTest
{
    private readonly Mock<IPatioRepository> _patioRepository;
    private readonly PatioUseCase _patioUseCase;

    public PatioUseCaseTest()
    {
        _patioRepository = new Mock<IPatioRepository>();
        _patioUseCase = new PatioUseCase(_patioRepository.Object);
    }

    [Fact]
    [Trait("UseCase", "Patios")]
    public async Task ObterTodosPatios_DeveRetonarTodosOsPatios()
    {
        //Arrange  
        var listaPatios = new PageResultModel<IEnumerable<PatioEntity>>
        {
            Data = new List<PatioEntity>
            {
                new PatioEntity { Id = 1, NomePatio = "Central" },
                new PatioEntity { Id = 2, NomePatio = "Teste" }
            },
            Deslocamento = 0,
            TotalRegistros = 2,
            RegistrosRetornado = 3,
        };

        _patioRepository.Setup(obj => obj.ObterTodosAsync(0, 3)).Returns(Task.FromResult(listaPatios));

        //Act  
        var resultado = await _patioUseCase.ObterTodosPatiosAsync();

        //Assert  
        Assert.NotNull(resultado);
        Assert.Equal(2, resultado.Value.Data.ToList().Count);
    }
    
    [Fact]  
    [Trait("UseCase", "Patios")]  
    public async Task ObterUmAsync_DeveRetonarTodosOsPatios()  
    {  
        //Arrange  
        var idPatio = 1;  
        var patio = new PatioEntity { Id = idPatio, NomePatio = "Central" };  
  
  
        _patioRepository.Setup(obj => obj.ObterUmAsync(idPatio)).Returns(Task.FromResult(patio)!);
  
        //Act  
        var resultado = await _patioUseCase.ObterUmPatioAsync(idPatio);  
  
        //Assert  
        Assert.NotNull(resultado);  
        Assert.Equal(idPatio, resultado.Value!.Id);  
    }

    [Fact]  
    [Trait("UseCase", "Patios")]  
    public async Task AdicionarAsync_DeveRetonarTodosOsPatios()  
    {  
        //Arrange  
        var patioDto = new PatioDto("Central");  
      
        var entity = patioDto.ToPatioEntity();  
    
        _patioRepository.Setup(obj => obj.AdicionarAsync(It.IsAny<PatioEntity>())).Returns(Task.FromResult(entity)!);  
  
        //Act  
        var resultado = await _patioUseCase.AdicionarPatioAsync(patioDto);  
  
        //Assert  
        Assert.NotNull(resultado);  
        Assert.Equal(patioDto.NomePatio, resultado.Value!.NomePatio);  
    }

}