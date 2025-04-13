using InventoryTrackingSystem.DTOs.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Controllers.Users
{
    public interface IUserController
    {
        [HttpPost("register")]
        [AllowAnonymous]
        Task<IActionResult> Register(UserRegisterDTO dto);

        [HttpPost("login")]
        [AllowAnonymous]
        Task<IActionResult> Login(UserLoginDTO dto);

        [HttpGet("me")]
        [Authorize]
        Task<IActionResult> GetCurrentUser();

        [HttpGet("all-users")]
        [Authorize(Roles = "Bazaar")]
        Task<IActionResult> GetAllUsers();

        [HttpGet("by-username/{username}")]
        [Authorize(Roles = "Admin")]
        Task<IActionResult> GetUserByUsername(string username);

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,StoreUser")]
        Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDTO dto);

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        Task<IActionResult> DeleteUser(int id);
    }
}