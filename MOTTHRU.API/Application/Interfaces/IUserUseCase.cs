using MOTTHRU.API.Application.Dtos;
using MOTTHRU.API.Domain.Entities;

namespace MOTTHRU.API.Application.Interfaces
{
    public interface IUserUseCase
    {
        Task<OperationResult<UserEntity?>> AutenticarUserAsync(UserDto entity);
    }
}