using InventoryTrackingSystem.Models;
using InventoryTrackingSystem.Repository;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryTrackingSystem.DTOs.StockMovementDTOs;

namespace InventoryTrackingSystem.Managers.StockMovements
{
    public class StockMovementManager : IStockMovementManager
    {
        private readonly IGenericRepository<StockMovement> _stockMovementRepository;
        private readonly IMapper _mapper;

        public StockMovementManager(IGenericRepository<StockMovement> stockMovementRepository, IMapper mapper)
        {
            _stockMovementRepository = stockMovementRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StockMovementDTO>> FilterByProductRangeAsync(int minProductId, int maxProductId)
        {
            var filteredMovements = await _stockMovementRepository.GetListAsync(sm =>
                sm.ProductId >= minProductId && sm.ProductId <= maxProductId);

            return _mapper.Map<IEnumerable<StockMovementDTO>>(filteredMovements);
        }


        public async Task<IEnumerable<StockMovementDTO>> FilterByStoreAsync(int storeId)
        {
            var filteredMovements = await _stockMovementRepository.GetListAsync(sm =>
                sm.StoreId == storeId);

            return _mapper.Map<IEnumerable<StockMovementDTO>>(filteredMovements);
        }


        public async Task<IEnumerable<StockMovementDTO>> FilterByMovementTypeAsync(string movementType)
        {
            var filteredMovements = await _stockMovementRepository.GetListAsync(sm =>
                sm.MovementType.Equals(movementType, StringComparison.OrdinalIgnoreCase));

            return _mapper.Map<IEnumerable<StockMovementDTO>>(filteredMovements);
        }


        public async Task<IEnumerable<StockMovementDTO>> FilterByQuantityRangeAsync(int minQuantity, int maxQuantity)
        {
            var filteredMovements = await _stockMovementRepository.GetListAsync(sm =>
                sm.Quantity >= minQuantity && sm.Quantity <= maxQuantity);

            return _mapper.Map<IEnumerable<StockMovementDTO>>(filteredMovements);
        }



        public async Task<IEnumerable<StockMovementDTO>> GetAllStockMovementsAsync()
        {
            try
            {
                var stockMovements = await _stockMovementRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<StockMovementDTO>>(stockMovements);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while getting all stock movements.", ex);
            }
        }

        public async Task<StockMovementDTO> GetStockMovementByIdAsync(int id)
        {
            try
            {
                var stockMovement = await _stockMovementRepository.GetByIdAsync(id);
                return _mapper.Map<StockMovementDTO>(stockMovement);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while getting the stock movement with ID {id}.", ex);
            }
        }

        public async Task<StockMovementDTO> AddStockMovementAsync(StockMovementCreateDTO stockMovementCreateDTO)
        {
            try
            {
                var stockMovement = _mapper.Map<StockMovement>(stockMovementCreateDTO);
                await _stockMovementRepository.AddAsync(stockMovement);
                return _mapper.Map<StockMovementDTO>(stockMovement);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while adding a new stock movement.", ex);
            }
        }

        public async Task<StockMovementDTO> UpdateStockMovementAsync(StockMovementDTO stockMovementDTO)
        {
            try
            {
                var stockMovement = _mapper.Map<StockMovement>(stockMovementDTO);
                await _stockMovementRepository.UpdateAsync(stockMovement);
                return _mapper.Map<StockMovementDTO>(stockMovement);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while updating the stock movement.", ex);
            }
        }

        public async Task DeleteStockMovementAsync(int id)
        {
            try
            {
                await _stockMovementRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while deleting the stock movement with ID {id}.", ex);
            }
        }


        public async Task<IEnumerable<StockMovementReportDTO>> GetAggregatedReportAsync(int storeId, DateTime startDate, DateTime endDate)
        {
            // Fetch stock movements for the given store and date range
            var movements = await _stockMovementRepository.GetListAsync(sm =>
                sm.StoreId == storeId &&
                sm.Timestamp >= startDate &&
                sm.Timestamp <= endDate);

            // Group by ProductId and calculate totals for "IN" and "OUT" movements
            var report = movements
                .GroupBy(sm => sm.ProductId)
                .Select(g => new StockMovementReportDTO
                {
                    ProductId = g.Key,
                    TotalIn = g.Where(sm => sm.MovementType == "IN").Sum(sm => sm.Quantity),
                    TotalOut = g.Where(sm => sm.MovementType == "OUT").Sum(sm => sm.Quantity)
                });

            return report;
        }

    }
}
