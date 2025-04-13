using InventoryTrackingSystem.DTOs.StoreProductStockDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryTrackingSystem.Managers.StoreProductStocks
{
    public interface IStoreProductStockManager
    {
        Task<IEnumerable<StoreProductStockDTO>> GetAllStoreProductStocksAsync();
        Task<StoreProductStockDTO> GetStoreProductStockByIdAsync(int id);
        Task<StoreProductStockDTO> AddStoreProductStockAsync(StoreProductStockCreateDTO storeProductStockCreateDTO);
        Task<StoreProductStockDTO> UpdateStoreProductStockAsync(StoreProductStockDTO storeProductStockDTO);
        Task DeleteStoreProductStockAsync(int id);
    }
}

