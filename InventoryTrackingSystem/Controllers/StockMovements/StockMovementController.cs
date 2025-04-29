using InventoryTrackingSystem.Managers.StockMovements;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryTrackingSystem.DTOs.StockMovementDTOs;
using Microsoft.AspNetCore.Authorization;

namespace InventoryTrackingSystem.Controllers.StockMovements
{
    [Route("api/stockmovements")]
    [ApiController]
    [Authorize] // Class-level authorization for all endpoints
    public class StockMovementController : ControllerBase, IStockMovementController
    {
        private readonly IStockMovementManager _stockMovementManager;

        public StockMovementController(IStockMovementManager stockMovementManager)
        {
            _stockMovementManager = stockMovementManager;
        }

        [HttpGet("filter-by-product-id-range")]
        [Authorize(Roles = "Bazaar,Admin")]
        public async Task<ActionResult<IEnumerable<StockMovementDTO>>> FilterByProductRange(
            [FromQuery] int minProductId, [FromQuery] int maxProductId)
        {
            var result = await _stockMovementManager.FilterByProductRangeAsync(minProductId, maxProductId);
            return Ok(result);
        }

        [HttpGet("filter-by-store")]
        [Authorize(Roles = "Bazaar,Admin")]
        public async Task<ActionResult<IEnumerable<StockMovementDTO>>> FilterByStore(
            [FromQuery] int storeId)
        {
            var result = await _stockMovementManager.FilterByStoreAsync(storeId);
            return Ok(result);
        }

        [HttpGet("filter-by-movement-type")]
        [Authorize(Roles = "Bazaar,Admin")]
        public async Task<ActionResult<IEnumerable<StockMovementDTO>>> FilterByMovementType(
            [FromQuery] string movementType)
        {
            var result = await _stockMovementManager.FilterByMovementTypeAsync(movementType);
            return Ok(result);
        }

        [HttpGet("filter-by-quantity-range")]
        [Authorize(Roles = "Bazaar,Admin")]
        public async Task<ActionResult<IEnumerable<StockMovementDTO>>> FilterByQuantityRange(
            [FromQuery] int minQuantity,
            [FromQuery] int maxQuantity)
        {
            var result = await _stockMovementManager.FilterByQuantityRangeAsync(minQuantity, maxQuantity);
            return Ok(result);
        }

        [HttpGet("aggregated-report")]
        [Authorize(Roles = "Bazaar,Admin")]
        public async Task<ActionResult<IEnumerable<StockMovementReportDTO>>> GetAggregatedReport(
            [FromQuery] int storeId,[FromQuery] DateTime startDate,[FromQuery] DateTime endDate)
        {
            var result = await _stockMovementManager.GetAggregatedReportAsync(storeId, startDate, endDate);
            return Ok(result);
        }


        [HttpGet]
        [Authorize(Roles = "Bazaar,Admin")]
        public async Task<ActionResult<IEnumerable<StockMovementDTO>>> GetAll()
        {
            return Ok(await _stockMovementManager.GetAllStockMovementsAsync());
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Bazaar,Admin")]
        public async Task<ActionResult<StockMovementDTO>> GetById(int id)
        {
            var stockMovementDTO = await _stockMovementManager.GetStockMovementByIdAsync(id);
            if (stockMovementDTO == null) return NotFound();
            return Ok(stockMovementDTO);
        }
    }
}