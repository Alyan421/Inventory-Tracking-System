using InventoryTrackingSystem.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryTrackingSystem.Managers.Users
{
    public interface IUserManager
    {
        Task<UserResponseDTO> RegisterAsync(UserRegisterDTO dto);
        Task<string> LoginAsync(UserLoginDTO dto);
        Task<UserResponseDTO> GetByUsernameAsync(string username);
        Task<IEnumerable<UserResponseDTO>> GetAllUsersAsync();
        Task<UserUpdateDTO> UpdateUserAsync(int id, UserUpdateDTO dto);
        Task<bool> DeleteUserAsync(int id);
    }
}