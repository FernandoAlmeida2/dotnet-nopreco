using dotnet_nopreco.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_nopreco.Repositories.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<int> InsertOne(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user.Id;
        }

        public async Task<User?> FindByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync((u) => u.Email == email);
        }

    }
}