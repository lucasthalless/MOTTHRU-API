using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MOTTHRU.API.Domain.Interfaces;
using MOTTHRU.API.Infrastructure.Data.AppData;
using MOTTHRU.API.Infrastructure.Data.Repository;
using Swashbuckle.AspNetCore.Filters;
using System.Threading.RateLimiting;
using MOTTHRU.API.Application.Interfaces;
using MOTTHRU.API.Application.UseCases;

var builder = WebApplication.CreateBuilder(args);

// Postgres DB Connection
builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
});

// Repositórios (injeção de dependências)
builder.Services.AddTransient<IMotoRepository, MotoRepository>();
builder.Services.AddTransient<IPatioRepository, PatioRepository>();
builder.Services.AddTransient<IRfidRepository, RfidRepository>();

builder.Services.AddTransient<IMotoUseCase, MotoUseCase>();
builder.Services.AddTransient<IPatioUseCase, PatioUseCase>();
builder.Services.AddTransient<IRfidUseCase, RfidUseCase>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger + Examples
builder.Services.AddSwaggerGen(conf =>
{
    conf.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MOTTHRU API",
        Version = "v1",
        Description = "API para cadastro e controle de Motos, Pátios e RFID."
    });
    conf.EnableAnnotations();
    conf.ExampleFilters();
});
builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();

// Rate Limiting
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter(policyName: "rateLimitePolicy", opt =>
    {
        opt.PermitLimit = 5; // até 5 requisições
        opt.Window = TimeSpan.FromSeconds(20); // em 20 segundos
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 2; // até 2 na fila
    });
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

// Response Compression
builder.Services.AddResponseCompression(options =>
{
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
});
builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = System.IO.Compression.CompressionLevel.Fastest;
});
builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = System.IO.Compression.CompressionLevel.Fastest;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MOTTHRU API V1");
        c.RoutePrefix = "swagger";
    });
}

app.UseCors("AllowAll");
app.UseAuthorization();
app.UseRateLimiter();
app.UseResponseCompression();

app.MapControllers();

app.Run();
