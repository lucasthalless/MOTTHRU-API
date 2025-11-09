using MOTTHRU.API.Domain.Entities;
using MOTTHRU.API.Domain.Errors;
using MOTTHRU.API.Domain.Interfaces;

namespace MOTTHRU.API.Infrastructure.Data.Repository;

public class UserRepository : IUserRepository
{
    public async Task<UserEntity?> AutenticarAsync(string user, string password)
    {
        // Simulando os dados do usuario no banco
        var listUser = new List<UserEntity>
        {
            new UserEntity { Id = 1, UserName = "user1", PasswordHash = "123456", Role = "admin" },
            new UserEntity { Id = 2, UserName = "user2", PasswordHash = "123456", Role = "user" }
        };

        var userAuth = listUser.FirstOrDefault(x =>
            x.UserName.ToLower() == user.ToLower() &&
            x.PasswordHash.ToLower() == password.ToLower()
        );

        if (userAuth is null)
            throw new NoContentException("Usuario não encontrado");

        return userAuth;
    }
}
