using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MOTTHRU.API.Application.Dtos;
using MOTTHRU.API.Application.Interfaces;
using MOTTHRU.API.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;
using MOTTHRU.API.Domain.Interfaces;

namespace MOTTHRU.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotoController : ControllerBase
    {
        private readonly IMotoApplicationService _motoApplicationService;
        
        [HttpGet]
        [SwaggerOperation(
            Summary = "Obter todas as motos",
            Description = "")]
        [SwaggerResponse(200, "Moto retornada com sucesso", typeof(List<MotoEntity>))]
        [SwaggerResponse((int)HttpStatusCode.NoContent, "Moto não encontrada")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Falha para obter as motos")]

        public IActionResult Get()
        {
            try
            {
                var motos = _motoApplicationService.GetAll();

                if (motos is null)
                    return NoContent();

                return Ok(motos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Obter moto por ID",
            Description = "Retorna os dados de uma moto específica, com base no ID informado"
        )]
        [SwaggerResponse(200, "Moto retornada com sucesso", typeof(MotoEntity))]
        [SwaggerResponse((int)HttpStatusCode.NoContent, "Moto não encontrada")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro ao obter a moto")]
        public IActionResult GetById(int id)
        {
            try
            {
                var moto = _motoApplicationService.GetMotoById(id);

                if (moto is null)
                    return NoContent();

                return Ok(moto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("byPatio")]
        [SwaggerOperation(
            Summary = "Obter motos por ID do pátio",
            Description = "Retorna uma lista de motos associadas ao ID do pátio informado"
        )]
        [SwaggerResponse(200, "Motos retornadas com sucesso", typeof(List<MotoEntity>))]
        [SwaggerResponse((int)HttpStatusCode.NoContent, "Nenhuma moto encontrada para o ID do pátio informado")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro ao obter as motos")]
        public IActionResult GetByIdPatio([FromQuery] string idPatio)
        {
            try
            {
                var motos = _motoApplicationService.GetMotosByIdPatio(idPatio);
                if (motos is null || !motos.Any())
                    return NoContent();

                return Ok(motos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("byStatus")]
        
        [SwaggerOperation(
            Summary = "Obter motos por status",
            Description = "Retorna uma lista de motos com o status informado"
        )]
        [SwaggerResponse(200, "Motos retornadas com sucesso", typeof(List<MotoEntity>))]
        [SwaggerResponse((int)HttpStatusCode.NoContent, "Nenhuma moto com o status informado")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro ao obter as motos")]
        public IActionResult GetByStatus([FromQuery] string status)
        {
            try
            {
                var motos = _motoApplicationService.GetMotosByStatus(status);
                if (motos is null || !motos.Any())
                    return NoContent();

                return Ok(motos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost]
        [SwaggerOperation(
            Summary = "Cadastrar nova moto",
            Description = "Cria uma nova moto no sistema com base nos dados enviados"
        )]
        [SwaggerResponse(201, "Moto criada com sucesso", typeof(MotoEntity))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro ao criar a moto")]
        public IActionResult Post([FromBody] MotoDto entity)
        {
            try
            {
                var moto = _motoApplicationService.CreateMoto(entity);

                return CreatedAtAction(nameof(GetById), new { id = moto.id }, moto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Atualizar moto existente",
            Description = "Atualiza os dados de uma moto já cadastrada com base no ID"
        )]
        [SwaggerResponse(200, "Moto atualizada com sucesso", typeof(MotoEntity))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro ao atualizar a moto")]
        public IActionResult Put(int id, [FromBody] MotoDto entity)
        {
            try
            {
                var moto = _motoApplicationService.UpdateMoto(id, entity);

                return Ok(moto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Deletar moto",
            Description = "Remove uma moto do sistema com base no ID informado"
        )]
        [SwaggerResponse(200, "Moto removida com sucesso")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro ao remover a moto")]
        public IActionResult Delete(int id)
        {
            try
            {
                var moto = _motoApplicationService.DeleteMoto(id);

                return Ok(moto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
