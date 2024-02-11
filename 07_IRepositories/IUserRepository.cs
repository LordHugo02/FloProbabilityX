using ProbabilityX_API.Models;

namespace ProbabilityX_API.IRepositories
{
    public interface IUserRepository
    {
        Task<List<UserModel>> GetAllUsers();
        Task<UserModel> GetUserById(int userId);
        Task<int> AddUser(UserModel user);
        Task UpdateUser(UserModel user);
        Task DeleteUser(int userId);
    }
}
