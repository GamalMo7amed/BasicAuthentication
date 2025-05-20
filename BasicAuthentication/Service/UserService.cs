using BasicAuthentication.Models;
using BasicAuthenticationDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace BasicAuthentication.Service
{
    public class UserService : IUserService
    {
        private readonly UserDbContext _context;

        public UserService(UserDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if(user == null)
                return false;
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
            
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
           return await _context.Users.AsNoTracking().ToListAsync();   
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.Id);
            if(existingUser == null) return false;
            
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User?> ValidateUserAsync(string email, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u=>u.Email == email && u.Password == password);  
        }
    }
}
