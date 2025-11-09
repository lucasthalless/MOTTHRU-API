using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MOTTHRU.API.Application.Dtos;
using MOTTHRU.API.Application.Interfaces;

namespace MOTTHRU.API.Presentation.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserUseCase _userUseCase;
        private readonly IConfiguration _configuration;

        public UserController(IUserUseCase userUseCase, IConfiguration configuration)
        {
            _userUseCase = userUseCase;
            _configuration = configuration;
        }
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(UserDto entity)
        {
            var result = await _userUseCase.AutenticarUserAsync(entity);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);


            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_configuration["Secretkey"]!.ToString());

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, result.Value!.UserName.ToString()),
                    new Claim(ClaimTypes.Role, result.Value!.Role.ToString()),
                    new Claim("Teste", "ValorTeste"),
                })
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return StatusCode(result.StatusCode, new
            {
                user = result.Value.UserName,
                token = tokenHandler.WriteToken(token),
            });
        }

        
    }

}