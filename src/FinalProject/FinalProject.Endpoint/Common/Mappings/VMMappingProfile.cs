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
            CreateMap<OrderDto, OrderListViewModel>()
                .ForMember(z => z.Address, a => a.MapFrom(x => x.Address.Content))
                .ForMember(z => z.ServiceName, a => a.MapFrom(x => x.Service.Description));
            CreateMap<OrderDto, OrderEditViewModel>();
            CreateMap<CommentDto, CommentListViewModel>()
                .ForMember(z => z.ImageAddress, a => a.MapFrom(x => Path.Combine("Uploads", x.Image.FileName)));
            CreateMap<AddressDto, AddressViewModel>().ReverseMap();
        }
    }
}
