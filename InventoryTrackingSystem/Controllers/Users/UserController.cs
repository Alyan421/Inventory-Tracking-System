using InventoryTrackingSystem.DTOs.UserDTOs;
using InventoryTrackingSystem.Managers.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop.Infrastructure;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Controllers.Users
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        // 🔐 Only Admin)
        [HttpPost("RegisterAnyRole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterAnyRole(UserRegisterDTO dto)
        {
            if (dto.Role != "Admin" && dto.Role != "Bazaar" & dto.Role != "StoreUser")
                return BadRequest("Please Give a valid Role.");
            var result = await _userManager.RegisterAsync(dto);
            return Ok(result);
        }

        // ✅ Publicly accessible
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegisterDTO dto)
        {
            dto.Role = "StoreUser"; // Default role for new users
            var result = await _userManager.RegisterAsync(dto);
            return Ok(result);
        }

        // ✅ Publicly accessible
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDTO dto)
        {
            var token = await _userManager.LoginAsync(dto);
            if (token == null)
                return Unauthorized("Invalid credentials");

            return Ok(new { Token = token });
        }

        // 🔐 Only authenticated users (both Bazaar and Store)
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var username = User.Identity.Name;
            var user = await _userManager.GetByUsernameAsync(username);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // 🔐 Only users with role "Bazaar"
        [HttpGet("all-users")]
        [Authorize(Roles = "Bazaar,Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userManager.GetAllUsersAsync();
            return Ok(users);
        }

        // 🔐 Only users with role "Admin" and "Bazaar"
        [HttpGet("by-username/{username}")]
        [Authorize(Roles = "Bazaar,Admin")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var user = await _userManager.GetByUsernameAsync(username);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // 🔐 Only Authorized users
        [HttpPut("me")]
        [Authorize]
        public async Task<IActionResult> UpdateCurrentUser([FromBody] UserUpdateDTO dto)
        {
            // Get current user's ID from claims
            var currentUserId = int.Parse(User.FindFirst("UserId")?.Value);
            var updatedUser = await _userManager.UpdateUserAsync(currentUserId, dto);
            if (updatedUser == null)
                return NotFound();

            return Ok(updatedUser);
        }

        // 🔐 Only users with role "Admin"
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var success = await _userManager.DeleteUserAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
