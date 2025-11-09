using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Operador")]
    [Route("api/v1/patio")]
    [ApiController]
    public class PatioController : ControllerBase
    {
        private readonly IPatioUseCase _patioUseCase;

        public PatioController(IPatioUseCase patioUseCase)
        {
            _patioUseCase = patioUseCase;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Lista pátios",
            Description = "Retorna a lista completa de pátios cadastrados com suporte a paginação."
        )]
        [SwaggerResponse(200, "Lista retornada com sucesso", typeof(IEnumerable<PatioEntity>))]
        [SwaggerResponse(204, "Não possui dados de pátios")]
        [SwaggerResponseExample(200, typeof(PatioResponseListSample))]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Get(int Deslocamento = 0, int RegistrosRetornado = 10)
        {
            var result = await _patioUseCase.ObterTodosPatiosAsync(Deslocamento, RegistrosRetornado);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateoas = new
            {
                data = result.Value?.Data.Select(p => new
                {
                    p.Id,
                    p.NomePatio,
                    // p.Endereco,
                    links = new
                    {
                        self = Url.Action(nameof(Get), "Patio", new { id = p.Id }, Request.Scheme),
                        put = Url.Action(nameof(Put), "Patio", new { id = p.Id }, Request.Scheme),
                        delete = Url.Action(nameof(Delete), "Patio", new { id = p.Id }, Request.Scheme),
                    }
                }),
                links = new
                {
                    self = Url.Action(nameof(Get), "Patio", null, Request.Scheme),
                    create = Url.Action(nameof(Post), "Patio", null, Request.Scheme),
                },
                pagina = new
                {
                    result.Value?.Deslocamento,
                    result.Value?.RegistrosRetornado,
                    result.Value?.TotalRegistros
                }
            };

            return StatusCode(result.StatusCode, hateoas);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtém pátio por ID", Description = "Retorna o pátio correspondente ao ID informado.")]
        [SwaggerResponse(200, "Pátio encontrado", typeof(PatioEntity))]
        [SwaggerResponse(404, "Pátio não encontrado")]
        [SwaggerResponseExample(200, typeof(PatioResponseSample))]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _patioUseCase.ObterUmPatioAsync(id);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateoas = new
            {
                data = result.Value,
                links = new
                {
                    self = Url.Action(nameof(Get), "Patio", new { id }),
                    getAll = Url.Action(nameof(Get), "Patio", null),
                    put = Url.Action(nameof(Put), "Patio", new { id }),
                    delete = Url.Action(nameof(Delete), "Patio", new { id }),
                }
            };

            return StatusCode(result.StatusCode, hateoas);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Adiciona pátio", Description = "Adiciona um novo pátio ao sistema.")]
        [SwaggerRequestExample(typeof(PatioDto), typeof(PatioRequestSample))]
        [SwaggerResponse(201, "Pátio salvo com sucesso", typeof(PatioEntity))]
        [SwaggerResponseExample(201, typeof(PatioResponseSample))]
        public async Task<IActionResult> Post(PatioDto entity)
        {
            var result = await _patioUseCase.AdicionarPatioAsync(entity);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateoas = new
            {
                data = result.Value,
                links = new
                {
                    self = Url.Action(nameof(Get), "Patio", new { id = result.Value.Id }),
                    update = Url.Action(nameof(Put), "Patio", new { id = result.Value.Id }),
                    delete = Url.Action(nameof(Delete), "Patio", new { id = result.Value.Id }),
                    getAll = Url.Action(nameof(Get), "Patio", null),
                }
            };

            return CreatedAtAction(nameof(Get), new { id = result.Value.Id }, hateoas);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualiza pátio", Description = "Edita os dados de um pátio existente pelo ID.")]
        [SwaggerResponse(200, "Pátio atualizado com sucesso", typeof(PatioEntity))]
        [SwaggerResponse(404, "Pátio não encontrado")]
        public async Task<IActionResult> Put(int id, PatioDto entity)
        {
            var result = await _patioUseCase.EditarPatioAsync(id, entity);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateoas = new
            {
                data = result.Value,
                links = new
                {
                    self = Url.Action(nameof(Get), "Patio", new { id }),
                    getAll = Url.Action(nameof(Get), "Patio", null),
                    delete = Url.Action(nameof(Delete), "Patio", new { id }),
                }
            };

            return StatusCode(result.StatusCode, hateoas);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Remove pátio", Description = "Remove um pátio existente pelo ID.")]
        [SwaggerResponse(200, "Pátio removido com sucesso")]
        [SwaggerResponse(404, "Pátio não encontrado")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _patioUseCase.DeletarPatioAsync(id);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateoas = new
            {
                message = "Pátio removido com sucesso",
                links = new
                {
                    getAll = Url.Action(nameof(Get), "Patio", null),
                    create = Url.Action(nameof(Post), "Patio", null),
                }
            };

            return StatusCode(result.StatusCode, hateoas);
        }
    }
}
