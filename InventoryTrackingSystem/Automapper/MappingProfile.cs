using AutoMapper;
using InventoryTrackingSystem.Models;
using InventoryTrackingSystem.DTOs.ProductDTOs;
using InventoryTrackingSystem.DTOs.StockMovementDTOs;
using InventoryTrackingSystem.DTOs.StoreProductStockDTOs;
using InventoryTrackingSystem.DTOs.StoreDTOs;
using InventoryTrackingSystem.DTOs.UserDTOs;
namespace InventoryTrackingSystem.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();
            CreateMap<ProductCreateDTO, Product>();

            CreateMap<Store, StoreDTO>();
            CreateMap<StoreDTO, Store>();
            CreateMap<StoreCreateDTO, Store>();

            CreateMap<StockMovement, StockMovementDTO>();
            CreateMap<StockMovementDTO, StockMovement>();
            CreateMap<StockMovementCreateDTO, StockMovement>();

            CreateMap<StoreProductStock, StoreProductStockDTO>();
            CreateMap<StoreProductStockDTO, StoreProductStock>();
            CreateMap<StoreProductStockCreateDTO, StoreProductStock>();

            CreateMap<User, UserResponseDTO>().ReverseMap();
            CreateMap<UserRegisterDTO, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
            CreateMap<UserUpdateDTO, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
        }
    }
}
