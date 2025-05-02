using InventoryTrackingSystem.Managers.StoreProductStocks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryTrackingSystem.DTOs.StoreProductStockDTOs;
using InventoryTrackingSystem.DTOs.StockMovementDTOs;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize(Roles = "Bazaar,Admin")]
        public async Task<ActionResult<IEnumerable<StoreProductStockDTO>>> GetAll()
        {
            return Ok(await _storeProductStockManager.GetAllStoreProductStocksAsync());
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Bazaar,Admin")]
        public async Task<ActionResult<StoreProductStockDTO>> GetById(int id)
        {
            var storeProductStockDTO = await _storeProductStockManager.GetStoreProductStockByIdAsync(id);
            if (storeProductStockDTO == null) return NotFound();
            return Ok(storeProductStockDTO);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Create(StoreProductStockCreateDTO storeProductStockCreateDTO)
        {
            var newStoreProductStockDTO = await _storeProductStockManager.AddStoreProductStockAsync(storeProductStockCreateDTO);
            return CreatedAtAction(nameof(GetById), new { id = newStoreProductStockDTO.Id }, newStoreProductStockDTO);
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult> Update(StockMovementCreateDTO createDTO)
        {
            var currentUserId = int.Parse(User.FindFirst("UserId")?.Value);
            var username = User.Identity.Name;

            var updatedStoreProductStockDTO = await _storeProductStockManager.UpdateStockAsync(createDTO, currentUserId, username);
            return Ok(updatedStoreProductStockDTO);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Bazaar,Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            await _storeProductStockManager.DeleteStoreProductStockAsync(id);
            return NoContent();
        }
    }
}