using InventoryTrackingSystem.DTOs.StoreProductStockDTOs;
using InventoryTrackingSystem.DTOs.StockMovementDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryTrackingSystem.Controllers.StoreProductStocks
{
    public interface IStoreProductStockController
    {
        [HttpGet]
        [Authorize(Roles = "Bazaar,Admin")]
        Task<ActionResult<IEnumerable<StoreProductStockDTO>>> GetAll();

        [HttpGet("{id}")]
        [Authorize(Roles = "Bazaar,Admin")]
        Task<ActionResult<StoreProductStockDTO>> GetById(int id);

        [HttpPost]
        [Authorize]
        Task<ActionResult> Create(StoreProductStockCreateDTO storeProductStockCreateDTO);

        [HttpPut]
        [Authorize]
        Task<ActionResult> Update(StockMovementCreateDTO createDTO);

        [HttpDelete("{id}")]
        [Authorize(Roles = "Bazaar,Admin")]
        Task<ActionResult> Delete(int id);
    }
}