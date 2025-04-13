namespace InventoryTrackingSystem.DTOs.StoreProductStockDTOs
{
    public class StoreProductStockCreateDTO
    {
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}

