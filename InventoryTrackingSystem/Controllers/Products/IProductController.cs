using InventoryTrackingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryTrackingSystem.DTOs.ProductDTOs;

namespace InventoryTrackingSystem.Controllers.Products
{
    public interface IProductController
    {
        Task<ActionResult<IEnumerable<ProductDTO>>> GetAll();
        Task<ActionResult<ProductDTO>> GetById(int id);
        Task<ActionResult> Create(ProductCreateDTO product);
        Task<ActionResult> Update(int id, ProductDTO product);
        Task<ActionResult> Delete(int id);
    }
}
