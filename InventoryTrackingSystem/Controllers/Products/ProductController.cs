using InventoryTrackingSystem.Managers.Products;
using InventoryTrackingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryTrackingSystem.DTOs.ProductDTOs;

namespace InventoryTrackingSystem.Controllers.Products
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase, IProductController
    {
        private readonly IProductManager _productManager;

        public ProductController(IProductManager productManager)
        {
            _productManager = productManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAll()
        {
            return Ok(await _productManager.GetAllProductsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetById(int id)
        {
            var productDTO = await _productManager.GetProductByIdAsync(id);
            if (productDTO == null) return NotFound();
            return Ok(productDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProductCreateDTO productDTO)
        {
            var newProductDTO = await _productManager.AddProductAsync(productDTO);
            return CreatedAtAction(nameof(GetById), new { id = newProductDTO.Id }, newProductDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, ProductDTO productDTO)
        {
            if (id != productDTO.Id) return BadRequest();
            var updatedProductDTO = await _productManager.UpdateProductAsync(productDTO);
            return Ok(updatedProductDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _productManager.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
