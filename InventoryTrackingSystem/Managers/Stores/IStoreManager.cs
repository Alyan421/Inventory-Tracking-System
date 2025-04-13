using InventoryTrackingSystem.DTOs.StoreDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryTrackingSystem.Managers.Stores
{
    public interface IStoreManager
    {
        Task<IEnumerable<StoreDTO>> GetAllStoresAsync();
        Task<StoreDTO> GetStoreByIdAsync(int id);
        Task<StoreDTO> AddStoreAsync(StoreCreateDTO storeCreateDTO);
        Task<StoreDTO> UpdateStoreAsync(StoreDTO storeDTO);
        Task DeleteStoreAsync(int id);
    }
}

