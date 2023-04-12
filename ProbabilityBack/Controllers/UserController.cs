using Microsoft.AspNetCore.Mvc;
using ProbabilityBack.Models;
using ProbabilityBack.Services.UserService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProbabilityBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetSingleUser(int id)
        {
            var user = await _userService.GetSingleUser(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<List<User>>> AddUser(User user)
        {
            var users = await _userService.AddUser(user);
            return Ok(users);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<User>>> UpdateUser(int id, User request)
        {
            var users = await _userService.UpdateUser(id, request);
            if (users == null)
                return NotFound();

            return Ok(users);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<User>>> DeleteUser(int id)
        {
            var users = await _userService.DeleteUser(id);
            if (users == null)
                return NotFound();

            return Ok(users);
        }
    }
}
