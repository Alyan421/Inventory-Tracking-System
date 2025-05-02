using InventoryTrackingSystem.DTOs.StoreDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryTrackingSystem.Controllers.Stores
{
    public interface IStoreController
    {
        [HttpGet]
        [Authorize(Roles = "Bazaar,Admin")]
        Task<ActionResult<IEnumerable<StoreDTO>>> GetAll();

        [HttpGet("{id}")]
        [Authorize(Roles = "Bazaar,Admin")]
        Task<ActionResult<StoreDTO>> GetById(int id);

        [HttpPost]
        [Authorize]
        Task<ActionResult> Create(StoreCreateDTO storeCreateDTO);

        [HttpPut("me")]
        [Authorize]
        Task<ActionResult> UpdateCurrentStore([FromBody] StoreDTO dto);

        [HttpDelete("{id}")]
        [Authorize(Roles = "Bazaar,Admin")]
        Task<ActionResult> Delete(int id);
    }
}