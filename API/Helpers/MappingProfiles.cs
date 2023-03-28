using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Looks at the properties in Product and attempts to map them to ProductToReturnDto
            // Name of properties and types must match
            // Needs to be added to program.cs as a service
            CreateMap<Product, ProductToReturnDto>()   
                
                // Configure mapping. We are affecting destination where something is being mapped to
                // In this case ProductToReturnDto
                // d is for destination? We want ProductBrand to be set to something
                // The 2nd parameter is o (options) 
                // s is the source
                // In other words set ProductBrand to be mapped from s.ProductBrand.Name
                // This will sort the type out as well
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>()); 

            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<AddressDto, Core.Entities.OrderAggregate.Address>();
        }
        
    }
}