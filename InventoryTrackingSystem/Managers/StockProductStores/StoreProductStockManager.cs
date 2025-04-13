using InventoryTrackingSystem.Models;
using InventoryTrackingSystem.Repository;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryTrackingSystem.DTOs.StoreProductStockDTOs;

namespace InventoryTrackingSystem.Managers.StoreProductStocks
{
    public class StoreProductStockManager : IStoreProductStockManager
    {
        private readonly IGenericRepository<StoreProductStock> _storeProductStockRepository;
        private readonly IMapper _mapper;

        public StoreProductStockManager(IGenericRepository<StoreProductStock> storeProductStockRepository, IMapper mapper)
        {
            _storeProductStockRepository = storeProductStockRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StoreProductStockDTO>> GetAllStoreProductStocksAsync()
        {
            try
            {
                var storeProductStocks = await _storeProductStockRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<StoreProductStockDTO>>(storeProductStocks);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while getting all store product stocks.", ex);
            }
        }

        public async Task<StoreProductStockDTO> GetStoreProductStockByIdAsync(int id)
        {
            try
            {
                var storeProductStock = await _storeProductStockRepository.GetByIdAsync(id);
                return _mapper.Map<StoreProductStockDTO>(storeProductStock);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while getting the store product stock with ID {id}.", ex);
            }
        }

        public async Task<StoreProductStockDTO> AddStoreProductStockAsync(StoreProductStockCreateDTO storeProductStockCreateDTO)
        {
            try
            {
                var storeProductStock = _mapper.Map<StoreProductStock>(storeProductStockCreateDTO);
                await _storeProductStockRepository.AddAsync(storeProductStock);
                return _mapper.Map<StoreProductStockDTO>(storeProductStock);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while adding a new store product stock.", ex);
            }
        }

        public async Task<StoreProductStockDTO> UpdateStoreProductStockAsync(StoreProductStockDTO storeProductStockDTO)
        {
            try
            {
                var storeProductStock = _mapper.Map<StoreProductStock>(storeProductStockDTO);
                await _storeProductStockRepository.UpdateAsync(storeProductStock);
                return _mapper.Map<StoreProductStockDTO>(storeProductStock);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while updating the store product stock.", ex);
            }
        }

        public async Task DeleteStoreProductStockAsync(int id)
        {
            try
            {
                await _storeProductStockRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while deleting the store product stock with ID {id}.", ex);
            }
        }
    }
}

