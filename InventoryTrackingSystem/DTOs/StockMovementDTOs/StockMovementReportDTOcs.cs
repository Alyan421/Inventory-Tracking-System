namespace InventoryTrackingSystem.DTOs.StockMovementDTOs
{
    public class StockMovementReportDTO
    {
        public int ProductId { get; set; }
        public int TotalIn { get; set; }
        public int TotalOut { get; set; }
    }
}
