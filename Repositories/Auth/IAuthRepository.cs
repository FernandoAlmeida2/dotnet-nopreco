using dotnet_nopreco.Models;

namespace dotnet_nopreco.Repositories.Auth
{
    public interface IAuthRepository
    {
        Task<int> InsertOne(User user);
        Task<User?> FindByName(string name);
        Task<User?> FindByEmail(string email);
    }
}