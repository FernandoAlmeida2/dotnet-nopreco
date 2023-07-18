using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using dotnet_nopreco.Dtos.User;
using dotnet_nopreco.Models;
using Microsoft.IdentityModel.Tokens;

namespace dotnet_nopreco.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepo;
        private readonly IConfiguration _configuration;

        public AuthService(IAuthRepository authRepo, IConfiguration configuration)
        {
            _authRepo = authRepo;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<LoginResponseDto>> Login(string email, string password)
        {
            var response = new ServiceResponse<LoginResponseDto>();

            var user = await _authRepo.FindByEmail(email);
            if (user is null || !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.HandleError("User/Password is incorrect.");
                return response;
            }

            response.Data = new LoginResponseDto {
                Name = user.Name,
                Token = CreateToken(user)
            };
            
            response.Message = "Login was successful.";

            return response;
        }

        public async Task<ServiceResponse<int>> SignUp(User user, string password)
        {
            var response = new ServiceResponse<int>();

            if (await _authRepo.FindByEmail(user.Email) is not null)
            {
                response.HandleError("User already exists.");
                return response;
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            response.Data = await _authRepo.InsertOne(user);
            response.Message = "User created successfully!";

            return response;
        }

        private void CreatePasswordHash(string password,
            out byte[] passwordHash,
            out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };
            var appSettingsToken = Environment.GetEnvironmentVariable("TokenJwt");

            if (appSettingsToken is null) throw new Exception("AppSettings Token is null!");
   
            SymmetricSecurityKey key =
                new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(appSettingsToken));

            SigningCredentials creds = new SigningCredentials(key,
                SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}