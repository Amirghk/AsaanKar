using AutoMapper;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Endpoint.Areas.Administration.Models;
using FinalProject.Endpoint.Areas.Identity.Models;

namespace FinalProject.Endpoint.Common.Mappings
{
    public class VMMappingProfile : Profile
    {
        public VMMappingProfile()
        {
            CreateMap<CustomerDto, CustomerListVM>().ReverseMap();
            CreateMap<ExpertDto, ExpertListVM>().ReverseMap();
            CreateMap<UploadDto, UploadViewModel>().ReverseMap();
        }
    }
}
