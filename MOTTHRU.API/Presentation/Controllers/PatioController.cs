using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using MOTTHRU.API.Application.Dtos;
using MOTTHRU.API.Application.Interfaces;
using MOTTHRU.API.Doc.Samples;
using MOTTHRU.API.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace MOTTHRU.API.Presentation.Controllers
{
    [Route("api/patio")]
    [ApiController]
    public class PatioController : ControllerBase
    {
        private readonly IPatioUseCase _patioUseCase;

        public PatioController(IPatioUseCase patioUseCase)
        {
            _patioUseCase = patioUseCase;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Lista pátios", Description = "Retorna a lista completa de pátios cadastrados.")]
        [SwaggerResponse(200, "Lista retornada com sucesso", typeof(IEnumerable<PatioEntity>))]
        [SwaggerResponseExample(200, typeof(PatioResponseListSample))]
        [SwaggerResponse(204, "Não possui dados de pátios")]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Get(int Deslocamento = 0, int RegistrosRetornado = 10)
        {
            var result = await _patioUseCase.ObterTodosPatiosAsync(Deslocamento, RegistrosRetornado);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            return StatusCode(result.StatusCode, result.Value);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtém pátio por ID", Description = "Retorna o pátio correspondente ao ID informado.")]
        [SwaggerResponse(200, "Pátio encontrado", typeof(PatioEntity))]
        [SwaggerResponseExample(200, typeof(PatioResponseSample))]
        [SwaggerResponse(404, "Pátio não encontrado")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _patioUseCase.ObterUmPatioAsync(id);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            return StatusCode(result.StatusCode, result.Value);
        }

        [HttpPost]
        [SwaggerRequestExample(typeof(PatioDto), typeof(PatioRequestSample))]
        [SwaggerResponse(200, "Pátio salvo com sucesso", typeof(PatioEntity))]
        [SwaggerResponseExample(200, typeof(PatioResponseSample))]
        public async Task<IActionResult> Post(PatioDto entity)
        {
            var result = await _patioUseCase.AdicionarPatioAsync(entity);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            return StatusCode(result.StatusCode, result.Value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, PatioDto entity)
        {
            var result = await _patioUseCase.EditarPatioAsync(id, entity);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            return StatusCode(result.StatusCode, result.Value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _patioUseCase.DeletarPatioAsync(id);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            return StatusCode(result.StatusCode, result.Value);
        }
    }
}
