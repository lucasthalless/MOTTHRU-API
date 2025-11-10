using System.Net;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using MOTTHRU.API.Application.Interfaces;
using MOTTHRU.API.Domain.Entities;

namespace Tests.APP;

public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>  
{  
    public const string Scheme = "TestAuth";  
  
    public TestAuthHandler(  
        IOptionsMonitor<AuthenticationSchemeOptions> options,  
        ILoggerFactory logger,  
        UrlEncoder encoder,  
        ISystemClock clock) : base(options, logger, encoder, clock) { }  
  
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()  
    {  
        var claims = new[]  
        {  
            new Claim(ClaimTypes.Name, "tester"),  
            new Claim(ClaimTypes.Role, "Operador"),
        };  
  
        var identity = new ClaimsIdentity(claims, Scheme);  
        var principal = new ClaimsPrincipal(identity);  
        var ticket = new AuthenticationTicket(principal, Scheme);  
  
        return Task.FromResult(AuthenticateResult.Success(ticket));  
    }  
}

public class CustomWebApplicationFactory : WebApplicationFactory<Program>  
{  
    public Mock<IPatioUseCase> PatioUseCaseMock { get; } = new();  
  
    protected override void ConfigureWebHost(Microsoft.AspNetCore.Hosting.IWebHostBuilder builder)  
    {  
        builder.ConfigureServices(services =>  
        {  
            // Cria o Mock do IPatioUseCase  
            services.RemoveAll(typeof(IPatioUseCase));  
            services.AddSingleton(PatioUseCaseMock.Object);  
  
            // Autenticação fake  
            services.AddAuthentication(options =>  
                {  
                    options.DefaultAuthenticateScheme = TestAuthHandler.Scheme;  
                    options.DefaultChallengeScheme = TestAuthHandler.Scheme;  
                })  
                .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(TestAuthHandler.Scheme, _ => { });  
        });  
    }  
}


public class PatioControllerTest : IClassFixture<CustomWebApplicationFactory>  
{
    private readonly CustomWebApplicationFactory _factory;  
  
    public PatioControllerTest(CustomWebApplicationFactory factory)  
    {  
        _factory = factory;  
    }
    
    [Fact(DisplayName = "GET /api/patio retorna 200 OK")]  
    [Trait("Controller", "Patios")]  
    public async Task Get_DeveRetornar200()  
    {  
        // Arrange  
        var retornoPatio = new PageResultModel<IEnumerable<PatioEntity>>  
        {  
            Data = new List<PatioEntity>  
            {  
                new PatioEntity  
                {  
                    Id = 1,  
                    NomePatio = "Central",
                }  
            },  
            Deslocamento = 0,  
            RegistrosRetornado = 1,  
            TotalRegistros = 1  
        };  
  
        var retorno = OperationResult<PageResultModel<IEnumerable<PatioEntity>>>.Success(retornoPatio, 200);  
  
        _factory.PatioUseCaseMock  
            .Setup(x => x.ObterTodosPatiosAsync(0, 3))  
            .ReturnsAsync(retorno);  
  
        using var client = _factory.CreateClient(); // já autenticado via handler  
  
        // Act    
        var response = await client.GetAsync("/api/patio");  
  
        // Assert  
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);  
    }


}