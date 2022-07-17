using AutoMapper;
using FinalProject.Application.Common.Dtos;
using FinalProject.Domain.Entities;

namespace FinalProject.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<City, CityDto>().ReverseMap();
        CreateMap<Province, ProvinceDto>().ReverseMap();
        CreateMap<Customer, CustomerDto>().ReverseMap();
        CreateMap<Comment, CommentDto>().ReverseMap();
        CreateMap<Upload, FileDetailDto>().ReverseMap();
        CreateMap<Address, AddressDto>().ReverseMap();
        CreateMap<Expert, ExpertDto>().ReverseMap();
        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<Category, ServiceDto>().ReverseMap();
        CreateMap<Service, SubServiceDto>().ReverseMap();
        CreateMap<Bid, SuggestionDto>().ReverseMap();
    }
}