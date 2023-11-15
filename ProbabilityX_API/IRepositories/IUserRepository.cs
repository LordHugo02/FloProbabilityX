using ProbabilityX_API.Models;

namespace ProbabilityX_API.IRepositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(int userId);
        Task<int> AddUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(int userId);
    }
}
