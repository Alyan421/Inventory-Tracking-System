using InventoryTrackingSystem.Models;
using InventoryTrackingSystem.Repository;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryTrackingSystem.DTOs.StoreProductStockDTOs;
using InventoryTrackingSystem.DTOs.StockMovementDTOs;
using InventoryTrackingSystem.Migrations;
using System.Security.Claims;

namespace InventoryTrackingSystem.Managers.StoreProductStocks
{
    public class StoreProductStockManager : IStoreProductStockManager
    {
        private readonly IGenericRepository<StoreProductStock> _storeProductStockRepository;
        private readonly IGenericRepository<StockMovement> _stockMovementRepository;
        private readonly IMapper _mapper;

        public StoreProductStockManager(
            IGenericRepository<StoreProductStock> storeProductStockRepository,
            IGenericRepository<StockMovement> stockMovementRepository,
            IMapper mapper)
        {
            _storeProductStockRepository = storeProductStockRepository;
            _stockMovementRepository = stockMovementRepository;
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

        public async Task<StoreProductStockDTO> UpdateStockAsync(StockMovementCreateDTO createDTO,int userId,string userName)
        {
            // Validate movement type first
            if (createDTO.MovementType != "IN" && createDTO.MovementType != "OUT")
            {
                throw new ArgumentException("Invalid movement type. Use 'IN' or 'OUT'.");
            }

            // Check for negative quantity
            if (createDTO.Quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero.");
            }

            // Find or create stock entry
            var stock = await _storeProductStockRepository.GetSingleAsync(sps =>
                sps.StoreId == createDTO.StoreID && sps.ProductId == createDTO.ProductId);

            if (stock == null)
            {
                // Only allow creation for "IN" movements
                if (createDTO.MovementType != "IN")
                {
                    throw new InvalidOperationException("Cannot perform OUT movement on non-existent stock.");
                }

                // Create new stock entry
                stock = new StoreProductStock
                {
                    StoreId = createDTO.StoreID,
                    ProductId = createDTO.ProductId,
                    Quantity = createDTO.Quantity
                };
                await _storeProductStockRepository.AddAsync(stock);
            }

            // Update existing stock
            if (createDTO.MovementType == "IN")
            {
                stock.Quantity += createDTO.Quantity;
            }
            else // OUT movement
            {
                if (stock.Quantity < createDTO.Quantity)
                {
                    throw new InvalidOperationException($"Insufficient stock. Available: {stock.Quantity}, Requested: {createDTO.Quantity}");
                }
                stock.Quantity -= createDTO.Quantity;
            }
            await _storeProductStockRepository.UpdateAsync(stock);

            // Record the movement
            var stockMovement = new StockMovement
            {
                StoreId = createDTO.StoreID,
                ProductId = createDTO.ProductId,
                Quantity = createDTO.Quantity,
                MovementType = createDTO.MovementType,
                Timestamp = DateTime.UtcNow,
                CreatedById = userId,
                CreatedByName = userName
            };
            await _stockMovementRepository.AddAsync(stockMovement);

            return _mapper.Map<StoreProductStockDTO>(stock);
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

