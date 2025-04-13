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
        public string CreatedBy { get; set; }
    }
}
