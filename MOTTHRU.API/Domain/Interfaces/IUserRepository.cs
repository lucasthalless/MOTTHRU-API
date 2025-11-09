using MOTTHRU.API.Domain.Entities;

namespace MOTTHRU.API.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<UserEntity?> AutenticarAsync(string user, string password);
    }
}