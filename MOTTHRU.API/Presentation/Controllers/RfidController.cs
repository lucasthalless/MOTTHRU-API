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
    [Route("api/rfid")]
    [ApiController]
    public class RfidController : ControllerBase
    {
        private readonly IRfidUseCase _rfidUseCase;

        public RfidController(IRfidUseCase rfidUseCase)
        {
            _rfidUseCase = rfidUseCase;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Lista RFIDs", Description = "Retorna a lista completa de RFIDs cadastrados.")]
        [SwaggerResponse(200, "Lista retornada com sucesso", typeof(IEnumerable<RfidEntity>))]
        [SwaggerResponseExample(200, typeof(RfidResponseListSample))]
        [SwaggerResponse(204, "Não possui dados de RFIDs")]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Get(int Deslocamento = 0, int RegistrosRetornado = 10)
        {
            var result = await _rfidUseCase.ObterTodosRfidsAsync(Deslocamento, RegistrosRetornado);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            return StatusCode(result.StatusCode, result.Value);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtém RFID por ID", Description = "Retorna o RFID correspondente ao ID informado.")]
        [SwaggerResponse(200, "RFID encontrado", typeof(RfidEntity))]
        [SwaggerResponseExample(200, typeof(RfidResponseSample))]
        [SwaggerResponse(404, "RFID não encontrado")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _rfidUseCase.ObterUmRfidAsync(id);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            return StatusCode(result.StatusCode, result.Value);
        }

        [HttpPost]
        [SwaggerRequestExample(typeof(RfidDto), typeof(RfidRequestSample))]
        [SwaggerResponse(200, "RFID salvo com sucesso", typeof(RfidEntity))]
        [SwaggerResponseExample(200, typeof(RfidResponseSample))]
        public async Task<IActionResult> Post(RfidDto entity)
        {
            var result = await _rfidUseCase.AdicionarRfidAsync(entity);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            return StatusCode(result.StatusCode, result.Value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, RfidDto entity)
        {
            var result = await _rfidUseCase.EditarRfidAsync(id, entity);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            return StatusCode(result.StatusCode, result.Value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _rfidUseCase.DeletarRfidAsync(id);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            return StatusCode(result.StatusCode, result.Value);
        }
    }
}
