﻿using AutoMapper;
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
            CreateMap<ExpertOrderDto, ExpertOrderViewModel>()
                .ForMember(z => z.ServiceName, a => a.MapFrom(x => x.Service.Description));
            CreateMap<ExpertPublicProfileDto, ExpertPublicProfileViewModel>()
                .ForMember(z => z.Services, a => a.MapFrom(x => x.Services.Select(x => x.Description)));

            CreateMap<UploadDto, UploadViewModel>().ReverseMap();


            CreateMap<OrderDto, OrderListViewModel>()
                .ForMember(z => z.ServiceName, a => a.MapFrom(x => x.Service.Description));
            CreateMap<OrderDto, OrderEditViewModel>();
            CreateMap<OrderSaveViewModel, OrderDto>();
            //.ForMember(z => z.State, a => a.MapFrom(x => OrderState.WaitingForExpertBid));

            CreateMap<CommentDto, CommentListViewModel>();
            CreateMap<CommentSaveViewModel, CommentDto>();


            CreateMap<CategoryDto, CategoryViewModel>();
            CreateMap<CategorySaveViewModel, CategoryDto>();


            CreateMap<ServiceListViewModel, ServiceDto>().ReverseMap()
                .ForMember(z => z.CategoryName, a => a.MapFrom(x => x.Category!.Name));
            CreateMap<ServiceSaveViewModel, ServiceDto>();

            CreateMap<AddressDto, AddressViewModel>().ReverseMap();

            CreateMap<BidDto, BidViewModel>().ReverseMap();
        }
    }
}
