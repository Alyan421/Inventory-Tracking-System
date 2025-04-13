namespace InventoryTrackingSystem.Models
{
    public class StoreProductStock
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}

