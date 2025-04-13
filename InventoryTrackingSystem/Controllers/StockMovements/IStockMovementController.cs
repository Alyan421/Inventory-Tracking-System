using InventoryTrackingSystem.DTOs.StockMovementDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryTrackingSystem.Controllers.StockMovements
{
    public interface IStockMovementController
    {
        Task<ActionResult<IEnumerable<StockMovementDTO>>> GetAll();
        Task<ActionResult<StockMovementDTO>> GetById(int id);
        Task<ActionResult> Create(StockMovementCreateDTO stockMovementCreateDTO);
        Task<ActionResult> Update(int id, StockMovementDTO stockMovementDTO);
        Task<ActionResult> Delete(int id);
    }
}

