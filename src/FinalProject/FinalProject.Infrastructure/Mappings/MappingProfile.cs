using AutoMapper;
using AutoMapper.EquivalencyExpression;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Domain.Entities;

namespace FinalProject.Infrastructure.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<City, CityDto>().ReverseMap();
        CreateMap<Province, ProvinceDto>().ReverseMap();
        CreateMap<Customer, CustomerDto>().ReverseMap();
        CreateMap<Comment, CommentDto>().ReverseMap();
        CreateMap<Upload, UploadDto>().ReverseMap();
        CreateMap<Address, AddressDto>().ReverseMap();
        CreateMap<Expert, ExpertDto>()
            .ReverseMap()
            .EqualityComparison((saveDto, entity) => saveDto.Id == entity.Id);
        CreateMap<Order, OrderDto>();
        CreateMap<OrderDto, Order>()
            .ForMember(z => z.Bids, opt => opt.Ignore())
            .ForMember(z => z.Customer, opt => opt.Ignore())
            .ForMember(z => z.Expert, opt => opt.Ignore())
            .ForMember(z => z.Service, opt => opt.Ignore());
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Service, ServiceDto>().ReverseMap();
        CreateMap<Bid, BidDto>().ReverseMap();
        CreateMap<OrderDto, ExpertOrderDto>().ReverseMap();
    }
}