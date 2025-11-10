using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using MOTTHRU.API.Application.Dtos;
using MOTTHRU.API.Application.Interfaces;
using MOTTHRU.API.Doc.Samples;
using MOTTHRU.API.Domain.Entities;
using MOTTHRU.API.Domain.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace MOTTHRU.API.Presentation.Controllers
{
    [Authorize(Roles = "Operador")]
    [Route("api/v1/rfid")]
    [ApiController]
    public class RfidController : ControllerBase
    {
        private readonly IRfidUseCase _rfidUseCase;
        private readonly IRfidAnomalyUseCase _rfidAnomalyUseCase;

        public RfidController(IRfidUseCase rfidUseCase, IRfidAnomalyUseCase rfidAnomalyUseCase)
        {
            _rfidUseCase = rfidUseCase;
            _rfidAnomalyUseCase = rfidAnomalyUseCase;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Lista RFIDs",
            Description = "Retorna a lista completa de RFIDs cadastrados com suporte a paginação."
        )]
        [SwaggerResponse(200, "Lista retornada com sucesso", typeof(IEnumerable<RfidEntity>))]
        [SwaggerResponse(204, "Não possui dados de RFIDs")]
        [SwaggerResponseExample(200, typeof(RfidResponseListSample))]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Get(int Deslocamento = 0, int RegistrosRetornado = 10)
        {
            var result = await _rfidUseCase.ObterTodosRfidsAsync(Deslocamento, RegistrosRetornado);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateoas = new
            {
                data = result.Value?.Data.Select(r => new
                {
                    r.Id,
                    r.Sinal,
                    links = new
                    {
                        self = Url.Action(nameof(Get), "Rfid", new { id = r.Id }, Request.Scheme),
                        put = Url.Action(nameof(Put), "Rfid", new { id = r.Id }, Request.Scheme),
                        delete = Url.Action(nameof(Delete), "Rfid", new { id = r.Id }, Request.Scheme),
                    }
                }),
                links = new
                {
                    self = Url.Action(nameof(Get), "Rfid", null, Request.Scheme),
                    create = Url.Action(nameof(Post), "Rfid", null, Request.Scheme),
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
        [SwaggerOperation(Summary = "Obtém RFID por ID", Description = "Retorna o RFID correspondente ao ID informado.")]
        [SwaggerResponse(200, "RFID encontrado", typeof(RfidEntity))]
        [SwaggerResponse(404, "RFID não encontrado")]
        [SwaggerResponseExample(200, typeof(RfidResponseSample))]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _rfidUseCase.ObterUmRfidAsync(id);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateoas = new
            {
                data = result.Value,
                links = new
                {
                    self = Url.Action(nameof(Get), "Rfid", new { id }),
                    getAll = Url.Action(nameof(Get), "Rfid", null),
                    put = Url.Action(nameof(Put), "Rfid", new { id }),
                    delete = Url.Action(nameof(Delete), "Rfid", new { id }),
                }
            };

            return StatusCode(result.StatusCode, hateoas);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Adiciona RFID", Description = "Adiciona um novo RFID ao sistema.")]
        [SwaggerRequestExample(typeof(RfidDto), typeof(RfidRequestSample))]
        [SwaggerResponse(201, "RFID salvo com sucesso", typeof(RfidEntity))]
        [SwaggerResponseExample(201, typeof(RfidResponseSample))]
        public async Task<IActionResult> Post(RfidDto entity)
        {
            var result = await _rfidUseCase.AdicionarRfidAsync(entity);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateoas = new
            {
                data = result.Value,
                links = new
                {
                    self = Url.Action(nameof(Get), "Rfid", new { id = result.Value.Id }),
                    update = Url.Action(nameof(Put), "Rfid", new { id = result.Value.Id }),
                    delete = Url.Action(nameof(Delete), "Rfid", new { id = result.Value.Id }),
                    getAll = Url.Action(nameof(Get), "Rfid", null),
                }
            };

            return CreatedAtAction(nameof(Get), new { id = result.Value.Id }, hateoas);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualiza RFID", Description = "Edita os dados de um RFID existente pelo ID.")]
        [SwaggerResponse(200, "RFID atualizado com sucesso", typeof(RfidEntity))]
        [SwaggerResponse(404, "RFID não encontrado")]
        public async Task<IActionResult> Put(int id, RfidDto entity)
        {
            var result = await _rfidUseCase.EditarRfidAsync(id, entity);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateoas = new
            {
                data = result.Value,
                links = new
                {
                    self = Url.Action(nameof(Get), "Rfid", new { id }),
                    getAll = Url.Action(nameof(Get), "Rfid", null),
                    delete = Url.Action(nameof(Delete), "Rfid", new { id }),
                }
            };

            return StatusCode(result.StatusCode, hateoas);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Remove RFID", Description = "Remove um RFID existente pelo ID.")]
        [SwaggerResponse(200, "RFID removido com sucesso")]
        [SwaggerResponse(404, "RFID não encontrado")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _rfidUseCase.DeletarRfidAsync(id);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateoas = new
            {
                message = "RFID removido com sucesso",
                links = new
                {
                    getAll = Url.Action(nameof(Get), "Rfid", null),
                    create = Url.Action(nameof(Post), "Rfid", null),
                }
            };

            return StatusCode(result.StatusCode, hateoas);
        }
        
        [HttpPost("anomaly-check")]
        public async Task<IActionResult> CheckAnomaly([FromBody] RfidSignalInput input)
        {
            if (input == null)
                return BadRequest("Entrada inválida.");

            bool isAnomaly = await _rfidAnomalyUseCase.ExecuteAsync(input.Sinal);

            return Ok(new
            {
                input.Sinal,
                Anomalia = isAnomaly,
                Mensagem = isAnomaly ? "Sinal anômalo detectado!" : "Sinal dentro do padrão."
            });
        }
    }
}
