using AutoMapper;
using LogisticsManagement.DataAccess.Models;
using LogisticsManagement.Service.DTOs;

namespace LogisticsManagement.Service.Convertors
{
    public class ApplicationMapper:Profile
    {
        public ApplicationMapper()
        {
            CreateMap<InventoryCategory,InventoryCategoryDTO>().ReverseMap();
            CreateMap<Inventory,InventoryDTO>().ReverseMap();
            CreateMap<VehicleType,VehicleTypeDTO>().ReverseMap();

            CreateMap<Vehicle, VehicleDTO>().ForMember(dest => dest.VehicleType, opt => opt.MapFrom(src => src.VehicleType.Type)).ReverseMap();

            CreateMap<User, UserWithDetailDTO>()
                .ForMember(dest => dest.UserDetailId, opt => opt.MapFrom(src => src.UserDetails.FirstOrDefault().Id))
                .ForMember(dest => dest.AddressId, opt => opt.MapFrom(src => src.UserDetails.FirstOrDefault().AddressId))
                .ForMember(dest => dest.LicenseNumber, opt => opt.MapFrom(src => src.UserDetails.FirstOrDefault().LicenseNumber))
                .ForMember(dest => dest.WareHouseId, opt => opt.MapFrom(src => src.UserDetails.FirstOrDefault().WareHouseId))
                .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => src.UserDetails.FirstOrDefault().IsAvailable))
                .ForMember(dest => dest.IsApproved, opt => opt.MapFrom(src => src.UserDetails.FirstOrDefault().IsApproved))
                .ReverseMap();

            CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.OrderDetailId, opt => opt.MapFrom(src => src.OrderDetails.FirstOrDefault().Id))
                .ForMember(dest => dest.InventoryId, opt => opt.MapFrom(src => src.OrderDetails.FirstOrDefault().InventoryId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.OrderDetails.FirstOrDefault().Quantity))
                .ForMember(dest => dest.SubTotal, opt => opt.MapFrom(src => src.OrderDetails.FirstOrDefault().SubTotal))
                .ForMember(dest => dest.OrderStatusId, opt => opt.MapFrom(src => src.OrderDetails.FirstOrDefault().OrderStatusId))
                .ForMember(dest => dest.OriginId, opt => opt.MapFrom(src => src.OrderDetails.FirstOrDefault().OriginId))
                .ForMember(dest => dest.DestinationId, opt => opt.MapFrom(src => src.OrderDetails.FirstOrDefault().DestinationId))
                .ForMember(dest => dest.ExpectedArrivalTime, opt => opt.MapFrom(src => src.OrderDetails.FirstOrDefault().ExpectedArrivalTime))
                .ForMember(dest => dest.ActualArrivalTime, opt => opt.MapFrom(src => src.OrderDetails.FirstOrDefault().ActualArrivalTime))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.OrderDetails.FirstOrDefault().OrderStatus.Id))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.OrderDetails.FirstOrDefault().OrderStatus.Status))
                .ReverseMap();

        }
    }
}
