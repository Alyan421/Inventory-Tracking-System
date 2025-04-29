namespace InventoryTrackingSystem.DTOs.StockMovementDTOs
{
    public class StockMovementCreateDTO
    {
        public int StoreID { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string MovementType { get; set; }
        public DateTime Timestamp { get; set; }
        public int CreatedById { get; set; } // ID of the user who created the movement
        public string CreatedByName { get; set; } // Name of the user who created the movement
    }
}
