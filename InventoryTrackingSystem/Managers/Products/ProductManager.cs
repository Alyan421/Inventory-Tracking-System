using InventoryTrackingSystem.Models;
using InventoryTrackingSystem.Repository;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryTrackingSystem.DTOs.ProductDTOs;

namespace InventoryTrackingSystem.Managers.Products
{
    public class ProductManager : IProductManager
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public ProductManager(IGenericRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            try
            {
                var products = await _productRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<ProductDTO>>(products);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while getting all products.", ex);
            }
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(id);
                return _mapper.Map<ProductDTO>(product);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while getting the product with ID {id}.", ex);
            }
        }

        public async Task<ProductDTO> AddProductAsync(ProductCreateDTO productDTO)
        {
            try
            {
                var product = _mapper.Map<Product>(productDTO);
                await _productRepository.AddAsync(product);
                return _mapper.Map<ProductDTO>(product);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while adding a new product.", ex);
            }
        }

        public async Task<ProductDTO> UpdateProductAsync(ProductDTO productDTO)
        {
            try
            {
                var product = _mapper.Map<Product>(productDTO);
                await _productRepository.UpdateAsync(product);
                return _mapper.Map<ProductDTO>(product);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while updating the product.", ex);
            }
        }

        public async Task DeleteProductAsync(int id)
        {
            try
            {
                await _productRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while deleting the product with ID {id}.", ex);
            }
        }
    }
}
