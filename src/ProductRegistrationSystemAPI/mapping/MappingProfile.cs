using AutoMapper;
using models;
using ProductRegistrationSystemAPI.data.entities;

namespace ProductRegistrationSystemAPI.mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductModel>()
            .ForMember(dest => dest.StatusName, opt => opt.Ignore())
            .ForMember(dest => dest.Discount, opt => opt.Ignore())
            .ForMember(dest => dest.FinalPrice, opt => opt.Ignore());
        CreateMap<ProductModel, Product>();
    }
}
