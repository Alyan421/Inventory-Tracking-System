using InventoryTrackingSystem.DTOs.StoreProductStockDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryTrackingSystem.Controllers.StoreProductStocks
{
    public interface IStoreProductStockController
    {
        Task<ActionResult<IEnumerable<StoreProductStockDTO>>> GetAll();
        Task<ActionResult<StoreProductStockDTO>> GetById(int id);
        Task<ActionResult> Create(StoreProductStockCreateDTO storeProductStockCreateDTO);
        Task<ActionResult> Update(int id, StoreProductStockDTO storeProductStockDTO);
        Task<ActionResult> Delete(int id);
    }
}

