using dotnet_nopreco.Dtos.User;
using dotnet_nopreco.Models;

namespace dotnet_nopreco.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> SignUp(User user, string password);
        Task<ServiceResponse<LoginResponseDto>> Login(string email, string password);
    }
}