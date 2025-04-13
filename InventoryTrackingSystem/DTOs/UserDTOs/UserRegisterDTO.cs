namespace InventoryTrackingSystem.DTOs.UserDTOs
{
    public class UserRegisterDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; } // e.g., "Admin", "User"
        public int? StoreId { get; set; } // Only required for StoreUser
    }
}
