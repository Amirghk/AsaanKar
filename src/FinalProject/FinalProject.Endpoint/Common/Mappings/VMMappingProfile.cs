using AutoMapper;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Domain.Enums;
using FinalProject.Endpoint.Areas.Administration.Models;
using FinalProject.Endpoint.Areas.Expert.Models;
using FinalProject.Endpoint.Areas.Identity.Models;
using FinalProject.Endpoint.Models;

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
                .ForMember(z => z.ServiceName, a => a.MapFrom(x => x.Service.Description));
            CreateMap<OrderDto, OrderEditViewModel>();
            CreateMap<CommentDto, CommentListViewModel>()
                .ForMember(z => z.ImageAddress, a => a.MapFrom(x => Path.Combine("Uploads", x.Image.FileName)));
            CreateMap<CategoryDto, CategoryViewModel>()
                .ForMember(z => z.ImageAddress, a => a.MapFrom(x => Path.Combine("Uploads", x.Picture.FileName)));
            CreateMap<ServiceViewModel, ServiceDto>().ReverseMap()
                .ForMember(z => z.CategoryName, a => a.MapFrom(x => x.Category!.Name));
            CreateMap<AddressDto, AddressViewModel>().ReverseMap();
            CreateMap<OrderSaveViewModel, OrderDto>()
                .ForMember(z => z.State, a => a.MapFrom(x => OrderState.WaitingForExpertBid));
            CreateMap<BidDto, BidViewModel>().ReverseMap();
            CreateMap<ExpertOrderDto, ExpertOrderViewModel>()
                .ForMember(z => z.ServiceName, a => a.MapFrom(x => x.Service.Description));
        }
    }
}
