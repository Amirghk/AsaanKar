using AutoMapper;
using FinalProject.Application.Common.Dtos;
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
