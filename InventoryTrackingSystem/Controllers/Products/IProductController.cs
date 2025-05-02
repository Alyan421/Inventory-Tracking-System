using InventoryTrackingSystem.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryTrackingSystem.Controllers.Products
{
    public interface IProductController
    {
        [HttpGet("All-products")]
        [Authorize(Roles = "Bazaar,Admin")]
        Task<ActionResult<IEnumerable<ProductDTO>>> GetAll();

        [HttpGet("{id}")]
        [Authorize(Roles = "Bazaar,Admin")]
        Task<ActionResult<ProductDTO>> GetById(int id);

        [HttpPost]
        [Authorize(Roles = "Bazaar,Admin")]
        Task<ActionResult> Create(ProductCreateDTO productDTO);

        [HttpPut("{id}")]
        [Authorize(Roles = "Bazaar,Admin")]
        Task<ActionResult> Update(int id, ProductDTO productDTO);

        [HttpDelete("{id}")]
        [Authorize(Roles = "Bazaar,Admin")]
        Task<ActionResult> Delete(int id);
    }
}