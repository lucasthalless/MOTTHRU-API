using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MOTTHRU.API.Application.Dtos;
using MOTTHRU.API.Application.Interfaces;
using MOTTHRU.API.Domain.Interfaces;

namespace MOTTHRU.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotoController : ControllerBase
    {
        private readonly IMotoApplicationService _motoApplicationService;
        
        [HttpGet]
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
        public IActionResult Post([FromBody] MotoDto entity)
        {
            try
            {
                var moto = _motoApplicationService.CreateMoto(entity);

                return Ok(moto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        [HttpPut("{id}")]
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
