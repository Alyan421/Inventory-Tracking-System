using InventoryTrackingSystem.DTOs.StockMovementDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryTrackingSystem.Controllers.StockMovements
{
    public interface IStockMovementController
    {
        [HttpGet("filter-by-product-id-range")]
        [Authorize(Roles = "Bazaar,Admin")]
        Task<ActionResult<IEnumerable<StockMovementDTO>>> FilterByProductRange(
            [FromQuery] int minProductId, [FromQuery] int maxProductId);

        [HttpGet("filter-by-store")]
        [Authorize(Roles = "Bazaar,Admin")]
        Task<ActionResult<IEnumerable<StockMovementDTO>>> FilterByStore(
            [FromQuery] int storeId);

        [HttpGet("filter-by-movement-type")]
        [Authorize(Roles = "Bazaar,Admin")]
        Task<ActionResult<IEnumerable<StockMovementDTO>>> FilterByMovementType(
            [FromQuery] string movementType);

        [HttpGet("filter-by-quantity-range")]
        [Authorize(Roles = "Bazaar,Admin")]
        Task<ActionResult<IEnumerable<StockMovementDTO>>> FilterByQuantityRange(
            [FromQuery] int minQuantity,
            [FromQuery] int maxQuantity);

        [HttpGet("aggregated-report")]
        [Authorize(Roles = "Bazaar,Admin")]
        Task<ActionResult<IEnumerable<StockMovementReportDTO>>> GetAggregatedReport(
               [FromQuery] int storeId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate);

        [HttpGet]
        [Authorize(Roles = "Bazaar,Admin")]
        Task<ActionResult<IEnumerable<StockMovementDTO>>> GetAll();

        [HttpGet("{id}")]
        [Authorize(Roles = "Bazaar,Admin")]
        Task<ActionResult<StockMovementDTO>> GetById(int id);
    }
}