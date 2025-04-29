using InventoryTrackingSystem.DTOs.StoreProductStockDTOs;
using InventoryTrackingSystem.DTOs.StockMovementDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryTrackingSystem.Managers.StoreProductStocks
{
    public interface IStoreProductStockManager
    {
        Task<IEnumerable<StoreProductStockDTO>> GetAllStoreProductStocksAsync();
        Task<StoreProductStockDTO> GetStoreProductStockByIdAsync(int id);
        Task<StoreProductStockDTO> AddStoreProductStockAsync(StoreProductStockCreateDTO storeProductStockCreateDTO);
        Task<StoreProductStockDTO> UpdateStockAsync(StockMovementCreateDTO createDTO,int userId,string userName);
        Task DeleteStoreProductStockAsync(int id);
    }
}

