namespace InventoryTrackingSystem.DTOs.ProductDTOs
{
    public class ProductCreateDTO
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
    }
}