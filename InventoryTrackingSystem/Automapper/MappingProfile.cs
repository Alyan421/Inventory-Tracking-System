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
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<ProductDTO, Product>().ReverseMap();
            CreateMap<ProductCreateDTO, Product>().ReverseMap();

            CreateMap<Store, StoreDTO>().ReverseMap();
            CreateMap<StoreDTO, Store>().ReverseMap();
            CreateMap<StoreCreateDTO, Store>();

            CreateMap<StockMovement, StockMovementDTO>().ReverseMap();
            CreateMap<StockMovementDTO, StockMovement>().ReverseMap();
            CreateMap<StockMovementCreateDTO, StockMovement>();
            CreateMap<StockMovementReportDTO, StockMovement>().ReverseMap();

            CreateMap<StoreProductStock, StoreProductStockDTO>();
            CreateMap<StoreProductStockDTO, StoreProductStock>();
            CreateMap<StoreProductStockCreateDTO, StoreProductStock>();

            CreateMap<User, UserResponseDTO>().ReverseMap();
            CreateMap<UserRegisterDTO, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
            CreateMap<UserUpdateDTO, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
            CreateMap<User, UserLoginDTO>().ReverseMap();
        }
    }
}
