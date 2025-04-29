namespace InventoryTrackingSystem.DTOs.UserDTOs
{
    public class UserUpdateDTO
    {
        public string Username { get; set; }
        public string Password { get; set; } // Optional, for password update
        public string Email { get; set; }
        public int? StoreId { get; set; } // Optional, for store assignment
    }
}