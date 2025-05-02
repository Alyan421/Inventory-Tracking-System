namespace InventoryTrackingSystem.Models
{
    public class StockMovement
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string MovementType { get; set; }
        public DateTime Timestamp { get; set; }
        public int CreatedById { get; set; }
        public string CreatedByName { get; set; } // Name of the user who created the movement
    }
}
