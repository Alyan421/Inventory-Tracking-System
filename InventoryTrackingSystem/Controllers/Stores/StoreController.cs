using InventoryTrackingSystem.Managers.Stores;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryTrackingSystem.DTOs.StoreDTOs;
using InventoryTrackingSystem.DTOs.StoreProductStockDTOs;

namespace InventoryTrackingSystem.Controllers.Stores
{
    [Route("api/stores")]
    [ApiController]
    public class StoreController : ControllerBase, IStoreController
    {
        private readonly IStoreManager _storeManager;

        public StoreController(IStoreManager storeManager)
        {
            _storeManager = storeManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoreDTO>>> GetAll()
        {
            return Ok(await _storeManager.GetAllStoresAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StoreDTO>> GetById(int id)
        {
            var storeDTO = await _storeManager.GetStoreByIdAsync(id);
            if (storeDTO == null) return NotFound();
            return Ok(storeDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Create(StoreCreateDTO storeCreateDTO)
        {
            var newStoreDTO = await _storeManager.AddStoreAsync(storeCreateDTO);
            return CreatedAtAction(nameof(GetById), new { id = newStoreDTO.Id }, newStoreDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, StoreDTO storeDTO)
        {
            if (id != storeDTO.Id) return BadRequest();
            var updatedStoreDTO = await _storeManager.UpdateStoreAsync(storeDTO);
            return Ok(updatedStoreDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _storeManager.DeleteStoreAsync(id);
            return NoContent();
        }
    }
}
