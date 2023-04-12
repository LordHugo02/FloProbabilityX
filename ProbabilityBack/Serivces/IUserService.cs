using ProbabilityBack.Models;

namespace ProbabilityBack.Services.UserService
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
        Task<User?> GetSingleUser(int id);
        Task<List<User>> AddUser(User hero);
        Task<List<User>?> UpdateUser(int id, User request);
        Task<List<User>?> DeleteUser(int id);
    }
}