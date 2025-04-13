using InventoryTrackingSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryTrackingSystem.DTOs.ProductDTOs;

namespace InventoryTrackingSystem.Managers.Products
{
    public interface IProductManager
    {
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        Task<ProductDTO> GetProductByIdAsync(int id);
        Task<ProductDTO> AddProductAsync(ProductCreateDTO product);
        Task<ProductDTO> UpdateProductAsync(ProductDTO product);
        Task DeleteProductAsync(int id);
    }
}
