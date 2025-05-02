using InventoryTrackingSystem.Managers.Stores;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryTrackingSystem.DTOs.StoreDTOs;
using InventoryTrackingSystem.DTOs.StoreProductStockDTOs;
using Microsoft.AspNetCore.Authorization; // Add this namespace for authorization

namespace InventoryTrackingSystem.Controllers.Stores
{
    [Route("api/stores")]
    [ApiController]
    [Authorize] // Require authentication for all endpoints by default
    public class StoreController : ControllerBase, IStoreController
    {
        private readonly IStoreManager _storeManager;

        public StoreController(IStoreManager storeManager)
        {
            _storeManager = storeManager;
        }

        [HttpGet]
        [Authorize(Roles = "Bazaar,Admin")] // Only Bazaar and Admin can access
        public async Task<ActionResult<IEnumerable<StoreDTO>>> GetAll()
        {
            return Ok(await _storeManager.GetAllStoresAsync());
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Bazaar,Admin")] // Only Bazaar and Admin can access
        public async Task<ActionResult<StoreDTO>> GetById(int id)
        {
            var storeDTO = await _storeManager.GetStoreByIdAsync(id);
            if (storeDTO == null) return NotFound();
            return Ok(storeDTO);
        }

        [HttpPost]
        [Authorize] // Or just remove this line since [Authorize] is at class level
        public async Task<ActionResult> Create(StoreCreateDTO storeCreateDTO)
        {
            var newStoreDTO = await _storeManager.AddStoreAsync(storeCreateDTO);
            return CreatedAtAction(nameof(GetById), new { id = newStoreDTO.Id }, newStoreDTO);
        }

        // ?? Only Authorized users
        [HttpPut("me")]
        [Authorize]
        public async Task<ActionResult> UpdateCurrentStore([FromBody] StoreDTO dto)
        {;
            var storeId = int.Parse(User.FindFirst("StoreId")?.Value);

            if (storeId != dto.Id)
                return Forbid(); // User is trying to update a different store


            var updatedStore = await _storeManager.UpdateStoreAsync(dto);
            if (updatedStore == null)
                return NotFound();

            return Ok(updatedStore);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Bazaar,Admin")] // Only Bazaar and Admin can access
        public async Task<ActionResult> Delete(int id)
        {
            await _storeManager.DeleteStoreAsync(id);
            return NoContent();
        }
    }
}