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
    [Route("api/v1/moto")]
    [ApiController]
    public class MotoController : ControllerBase
    {
        private readonly IMotoUseCase _motoUseCase;

        public MotoController(IMotoUseCase motoUseCase)
        {
            _motoUseCase = motoUseCase;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Lista motos",
            Description = "Retorna a lista completa de motos cadastradas com suporte a paginação."
        )]
        [SwaggerResponse(200, "Lista retornada com sucesso", typeof(IEnumerable<MotoEntity>))]
        [SwaggerResponse(204, "Não possui dados de motos")]
        [SwaggerResponseExample(200, typeof(MotoResponseListSample))]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Get(int Deslocamento = 0, int RegistrosRetornado = 10)
        {
            var result = await _motoUseCase.ObterTodasMotosAsync(Deslocamento, RegistrosRetornado);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateoas = new
            {
                data = result.Value?.Data.Select(m => new
                {
                    m.Id,
                    // m.Modelo,
                    m.Placa,
                    // m.Status,
                    Patio = m.Patio?.NomePatio ?? string.Empty,
                    // Rfid = m.Rfid?.Codigo ?? string.Empty,
                    links = new
                    {
                        self = Url.Action(nameof(Get), "Moto", new { id = m.Id }, Request.Scheme),
                        put = Url.Action(nameof(Put), "Moto", new { id = m.Id }, Request.Scheme),
                        delete = Url.Action(nameof(Delete), "Moto", new { id = m.Id }, Request.Scheme),
                    }
                }),
                links = new
                {
                    self = Url.Action(nameof(Get), "Moto", null, Request.Scheme),
                    create = Url.Action(nameof(Post), "Moto", null, Request.Scheme),
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
        [SwaggerOperation(
            Summary = "Obtém moto por ID",
            Description = "Retorna a moto correspondente ao ID informado."
        )]
        [SwaggerResponse(200, "Moto encontrada", typeof(MotoEntity))]
        [SwaggerResponse(404, "Moto não encontrada")]
        [SwaggerResponseExample(200, typeof(MotoResponseSample))]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _motoUseCase.ObterUmaMotoAsync(id);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateoas = new
            {
                data = result.Value,
                links = new
                {
                    self = Url.Action(nameof(Get), "Moto", new { id }),
                    getAll = Url.Action(nameof(Get), "Moto", null),
                    put = Url.Action(nameof(Put), "Moto", new { id }),
                    delete = Url.Action(nameof(Delete), "Moto", new { id }),
                }
            };

            return StatusCode(result.StatusCode, hateoas);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Adiciona moto",
            Description = "Adiciona uma nova moto ao sistema."
        )]
        [SwaggerRequestExample(typeof(MotoDto), typeof(MotoRequestSample))]
        [SwaggerResponse(201, "Moto salva com sucesso", typeof(MotoEntity))]
        [SwaggerResponseExample(201, typeof(MotoResponseSample))]
        public async Task<IActionResult> Post(MotoDto entity)
        {
            var result = await _motoUseCase.AdicionarMotoAsync(entity);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateoas = new
            {
                data = result.Value,
                links = new
                {
                    self = Url.Action(nameof(Get), "Moto", new { id = result.Value.Id }),
                    update = Url.Action(nameof(Put), "Moto", new { id = result.Value.Id }),
                    delete = Url.Action(nameof(Delete), "Moto", new { id = result.Value.Id }),
                    getAll = Url.Action(nameof(Get), "Moto", null),
                }
            };

            return CreatedAtAction(nameof(Get), new { id = result.Value.Id }, hateoas);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Atualiza moto",
            Description = "Edita os dados de uma moto existente pelo ID."
        )]
        [SwaggerResponse(200, "Moto atualizada com sucesso", typeof(MotoEntity))]
        [SwaggerResponse(404, "Moto não encontrada")]
        public async Task<IActionResult> Put(int id, MotoDto entity)
        {
            var result = await _motoUseCase.EditarMotoAsync(id, entity);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateoas = new
            {
                data = result.Value,
                links = new
                {
                    self = Url.Action(nameof(Get), "Moto", new { id }),
                    getAll = Url.Action(nameof(Get), "Moto", null),
                    delete = Url.Action(nameof(Delete), "Moto", new { id }),
                }
            };

            return StatusCode(result.StatusCode, hateoas);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Remove moto",
            Description = "Remove uma moto existente pelo ID."
        )]
        [SwaggerResponse(200, "Moto removida com sucesso")]
        [SwaggerResponse(404, "Moto não encontrada")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _motoUseCase.DeletarMotoAsync(id);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateoas = new
            {
                message = "Moto removida com sucesso",
                links = new
                {
                    getAll = Url.Action(nameof(Get), "Moto", null),
                    create = Url.Action(nameof(Post), "Moto", null),
                }
            };

            return StatusCode(result.StatusCode, hateoas);
        }
    }
}
