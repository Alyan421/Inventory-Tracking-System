using InventoryTrackingSystem.Managers.StockMovements;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryTrackingSystem.DTOs.StockMovementDTOs;

namespace InventoryTrackingSystem.Controllers.StockMovements
{
    [Route("api/stockmovements")]
    [ApiController]
    public class StockMovementController : ControllerBase, IStockMovementController
    {
        private readonly IStockMovementManager _stockMovementManager;

        public StockMovementController(IStockMovementManager stockMovementManager)
        {
            _stockMovementManager = stockMovementManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockMovementDTO>>> GetAll()
        {
            return Ok(await _stockMovementManager.GetAllStockMovementsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StockMovementDTO>> GetById(int id)
        {
            var stockMovementDTO = await _stockMovementManager.GetStockMovementByIdAsync(id);
            if (stockMovementDTO == null) return NotFound();
            return Ok(stockMovementDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Create(StockMovementCreateDTO stockMovementCreateDTO)
        {
            var newStockMovementDTO = await _stockMovementManager.AddStockMovementAsync(stockMovementCreateDTO);
            return CreatedAtAction(nameof(GetById), new { id = newStockMovementDTO.Id }, newStockMovementDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, StockMovementDTO stockMovementDTO)
        {
            if (id != stockMovementDTO.Id) return BadRequest();
            var updatedStockMovementDTO = await _stockMovementManager.UpdateStockMovementAsync(stockMovementDTO);
            return Ok(updatedStockMovementDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _stockMovementManager.DeleteStockMovementAsync(id);
            return NoContent();
        }
    }
}
