using InventoryTrackingSystem.Models;
using InventoryTrackingSystem.Repository;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryTrackingSystem.DTOs.StoreDTOs;

namespace InventoryTrackingSystem.Managers.Stores
{
    public class StoreManager : IStoreManager
    {
        private readonly IGenericRepository<Store> _storeRepository;
        private readonly IMapper _mapper;

        public StoreManager(IGenericRepository<Store> storeRepository, IMapper mapper)
        {
            _storeRepository = storeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StoreDTO>> GetAllStoresAsync()
        {
            try
            {
                var stores = await _storeRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<StoreDTO>>(stores);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while getting all stores.", ex);
            }
        }

        public async Task<StoreDTO> GetStoreByIdAsync(int id)
        {
            try
            {
                var store = await _storeRepository.GetByIdAsync(id);
                return _mapper.Map<StoreDTO>(store);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while getting the store with ID {id}.", ex);
            }
        }

        public async Task<StoreDTO> AddStoreAsync(StoreCreateDTO storeCreateDTO)
        {
            try
            {
                var store = _mapper.Map<Store>(storeCreateDTO);
                await _storeRepository.AddAsync(store);
                return _mapper.Map<StoreDTO>(store);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while adding a new store.", ex);
            }
        }

        public async Task<StoreDTO> UpdateStoreAsync(StoreDTO storeDTO)
        {
            try
            {
                var store = _mapper.Map<Store>(storeDTO);
                await _storeRepository.UpdateAsync(store);
                return _mapper.Map<StoreDTO>(store);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while updating the store.", ex);
            }
        }

        public async Task DeleteStoreAsync(int id)
        {
            try
            {
                await _storeRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while deleting the store with ID {id}.", ex);
            }
        }
    }
}
