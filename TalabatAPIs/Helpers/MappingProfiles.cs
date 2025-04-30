using AutoMapper;
using Talabat.Core.Entites;
using Talabat.Core.Entites.Identity;
using Talabat.Core.Entites.Order;
using TalabatAPIs.DTOs;

namespace TalabatAPIs.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDTO>()
                .ForMember(D => D.ProductType, O => O.MapFrom(S => S.ProductType.Name))
                .ForMember(D => D.ProductBrand, O => O.MapFrom(S => S.ProductBrand.Name))
                // To Resolve PictureUrl [Product] and store the Resolved string in PictureUrl [ProductToReturnDTO]
                .ForMember(D => D.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>());

            CreateMap<Talabat.Core.Entites.Identity.Address, AddressDto>().ReverseMap();

            CreateMap<CustomerBasketDto , CustomerBasket>().ReverseMap();
            CreateMap<BasketItemDto , BasketItem>().ReverseMap();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(D => D.DeliveryMethod, O => O.MapFrom(S => S.DeliveryMethod.ShortName))
                .ForMember(D => D.DeliveryMethodCost, O => O.MapFrom(S => S.DeliveryMethod.Coast))
                .ReverseMap();

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(D => D.PictureUrl, O => O.MapFrom<OrdedPictureResolver>());
        }
    }
}
