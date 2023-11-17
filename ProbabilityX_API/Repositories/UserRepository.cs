using ProbabilityX_API.IRepositories;
using ProbabilityX_API.Models;
using Microsoft.EntityFrameworkCore;
using ProbabilityX_API.IServices;
using ProbabilityX_API.Settings;

namespace ProbabilityX_API.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {

        public UserRepository(ProbabilityXContext context) : base(context)
        {
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserById(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<int> AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user.Id_User;
        }

        public async Task UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
