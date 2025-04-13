namespace InventoryTrackingSystem.DTOs.StockMovementDTOs
{
    public class StockMovementCreateDTO
    {
        public int StockID { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string MovementType { get; set; }
        public DateTime Timestamp { get; set; }
        public string CreatedBy { get; set; }
    }
}
