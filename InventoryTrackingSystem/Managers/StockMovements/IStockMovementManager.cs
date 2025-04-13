using InventoryTrackingSystem.DTOs.StockMovementDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryTrackingSystem.Managers.StockMovements
{
    public interface IStockMovementManager
    {
        Task<IEnumerable<StockMovementDTO>> GetAllStockMovementsAsync();
        Task<StockMovementDTO> GetStockMovementByIdAsync(int id);
        Task<StockMovementDTO> AddStockMovementAsync(StockMovementCreateDTO stockMovementCreateDTO);
        Task<StockMovementDTO> UpdateStockMovementAsync(StockMovementDTO stockMovementDTO);
        Task DeleteStockMovementAsync(int id);
    }
}

