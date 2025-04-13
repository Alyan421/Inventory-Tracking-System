using InventoryTrackingSystem.DTOs.StoreDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryTrackingSystem.Controllers.Stores
{
    public interface IStoreController
    {
        Task<ActionResult<IEnumerable<StoreDTO>>> GetAll();
        Task<ActionResult<StoreDTO>> GetById(int id);
        Task<ActionResult> Create(StoreCreateDTO storeCreateDTO);
        Task<ActionResult> Update(int id, StoreDTO storeDTO);
        Task<ActionResult> Delete(int id);
    }
}

