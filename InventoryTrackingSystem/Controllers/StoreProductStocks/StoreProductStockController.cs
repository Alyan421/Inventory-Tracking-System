using InventoryTrackingSystem.Managers.StoreProductStocks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryTrackingSystem.DTOs.StoreProductStockDTOs;

namespace InventoryTrackingSystem.Controllers.StoreProductStocks
{
    [Route("api/storeproductstocks")]
    [ApiController]
    public class StoreProductStockController : ControllerBase, IStoreProductStockController
    {
        private readonly IStoreProductStockManager _storeProductStockManager;

        public StoreProductStockController(IStoreProductStockManager storeProductStockManager)
        {
            _storeProductStockManager = storeProductStockManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoreProductStockDTO>>> GetAll()
        {
            return Ok(await _storeProductStockManager.GetAllStoreProductStocksAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StoreProductStockDTO>> GetById(int id)
        {
            var storeProductStockDTO = await _storeProductStockManager.GetStoreProductStockByIdAsync(id);
            if (storeProductStockDTO == null) return NotFound();
            return Ok(storeProductStockDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Create(StoreProductStockCreateDTO storeProductStockCreateDTO)
        {
            var newStoreProductStockDTO = await _storeProductStockManager.AddStoreProductStockAsync(storeProductStockCreateDTO);
            return CreatedAtAction(nameof(GetById), new { id = newStoreProductStockDTO.Id }, newStoreProductStockDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, StoreProductStockDTO storeProductStockDTO)
        {
            if (id != storeProductStockDTO.Id) return BadRequest();
            var updatedStoreProductStockDTO = await _storeProductStockManager.UpdateStoreProductStockAsync(storeProductStockDTO);
            return Ok(updatedStoreProductStockDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _storeProductStockManager.DeleteStoreProductStockAsync(id);
            return NoContent();
        }
    }
}

