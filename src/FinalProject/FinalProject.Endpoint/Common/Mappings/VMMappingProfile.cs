using AutoMapper;
using FinalProject.Domain.Dtos;
using FinalProject.Endpoint.Areas.Administration.Models;

namespace FinalProject.Endpoint.Common.Mappings
{
    public class VMMappingProfile : Profile
    {
        public VMMappingProfile()
        {
            CreateMap<CustomerDto, CustomerListVM>().ReverseMap();
            CreateMap<ExpertDto, ExpertListVM>().ReverseMap();
        }
    }
}
