using Microsoft.AspNetCore.Mvc;
using ProbabilityX_API.Models;

namespace ProbabilityX_API.IServices
{
    public interface IUserService
    {
        Task<List<UserModel>> GetAllUsers();
        Task<UserModel> GetUserById(int userId);
        Task<int> AddUser(UserModel user);
        Task UpdateUser(UserModel user);
        Task DeleteUser(int userId);
    }
}
