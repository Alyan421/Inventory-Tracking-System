using InventoryTrackingSystem.DTOs.StockMovementDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryTrackingSystem.Managers.StockMovements
{
    public interface IStockMovementManager
    {
        Task<IEnumerable<StockMovementDTO>> FilterByProductRangeAsync(int minProductId, int maxProductId);
        Task<IEnumerable<StockMovementDTO>> FilterByStoreAsync(int storeId);
        Task<IEnumerable<StockMovementDTO>> FilterByMovementTypeAsync(string movementType);
        Task<IEnumerable<StockMovementDTO>> FilterByQuantityRangeAsync(int minQuantity, int maxQuantity);
        Task<IEnumerable<StockMovementReportDTO>> GetAggregatedReportAsync(int storeId, DateTime startDate, DateTime endDate);

        Task<IEnumerable<StockMovementDTO>> GetAllStockMovementsAsync();
        Task<StockMovementDTO> GetStockMovementByIdAsync(int id);
        Task<StockMovementDTO> AddStockMovementAsync(StockMovementCreateDTO stockMovementCreateDTO);
        Task<StockMovementDTO> UpdateStockMovementAsync(StockMovementDTO stockMovementDTO);
        Task DeleteStockMovementAsync(int id);
    }
}

