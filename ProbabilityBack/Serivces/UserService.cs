using ProbabilityBack.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProbabilityBack.Data;

namespace ProbabilityBack.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<User>> AddUser(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return await _context.User.ToListAsync();
        }

        public async Task<List<User>?> DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user is null)
                return null;

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return await _context.User.ToListAsync();
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _context.User.ToListAsync();
            return users;
        }

        public async Task<User?> GetSingleUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user is null)
                return null;

            return user;
        }

        public async Task<List<User>?> UpdateUser(int id, User request)
        {
            var user = await _context.User.FindAsync(id);
            if (user is null)
                return null;

            user.Firstname = request.Firstname;
            user.Lastname = request.Lastname;
            user.Surname = request.Surname;
            user.Location = request.Location;

            await _context.SaveChangesAsync();

            return await _context.User.ToListAsync();
        }
    }
}
