using Microsoft.AspNetCore.Mvc;
using ProbabilityX_API.IRepositories;
using ProbabilityX_API.IServices;
using ProbabilityX_API.Models;

namespace ProbabilityX_API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }

        public async Task<User> GetUserById(int userId)
        {
            return await _userRepository.GetUserById(userId);
        }

        public async Task<int> AddUser(User user)
        {
            // Tu peux ajouter ici des règles métier ou de validation avant d'ajouter l'utilisateur
            return await _userRepository.AddUser(user);
        }

        public async Task UpdateUser(User user)
        {
            // Tu peux ajouter ici des règles métier ou de validation avant de mettre à jour l'utilisateur
            await _userRepository.UpdateUser(user);
        }

        public async Task DeleteUser(int userId)
        {
            // Tu peux ajouter ici des règles métier ou de validation avant de supprimer l'utilisateur
            await _userRepository.DeleteUser(userId);
        }
    }
}
