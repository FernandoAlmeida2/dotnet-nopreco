using dotnet_nopreco.Dtos.User;
using dotnet_nopreco.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_nopreco.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("signin")]
        public async Task<ActionResult<ServiceResponse<LoginResponseDto>>> Login(UserLoginDto user)
        {
            var response = await _authService.Login(user.Email, user.Password);

            if(!response.Success)
            {
                if(response.Message == "User/Password is incorrect.")
                    return Unauthorized(response);
            }
            return Ok(response);
        }

        [HttpPost("signup")]
        public async Task<ActionResult<ServiceResponse<int>>> SignUp(UserRegisterDto newUserDto)
        {    
            var newUser = new User
            {
                Name = newUserDto.Name,
                Email = newUserDto.Email,
            };

            var response = await  _authService.SignUp(newUser, newUserDto.Password);

            if (!response.Success)
            {
                if(response.Message == "Admin user already exists.") return Conflict(response);
            }
            return Created("Create an user", response);
        }
    }
}