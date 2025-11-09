using MOTTHRU.API.Application.Dtos;
using MOTTHRU.API.Application.Interfaces;
using MOTTHRU.API.Domain.Entities;
using MOTTHRU.API.Domain.Interfaces;

namespace MOTTHRU.API.Application.UseCases
{
    public class UserUseCase : IUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public UserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<OperationResult<UserEntity?>> AutenticarUserAsync(UserDto entity)
        {
            try
            {
                var userAuth = await _userRepository.AutenticarAsync(entity.user, entity.password);

                return OperationResult<UserEntity?>.Success(userAuth);
            }
            catch (Exception)
            {
                return OperationResult<UserEntity?>.Failure("Ocorreu um erro ao buscar o cliente");
            }
        }
    }

}