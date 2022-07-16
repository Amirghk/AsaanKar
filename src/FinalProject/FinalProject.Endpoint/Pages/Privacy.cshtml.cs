using FinalProject.Application.Common.Dtos;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Endpoint.Pages;

public class PrivacyModel : PageModel
{
    private readonly ILogger<PrivacyModel> _logger;
    private readonly IAddressService _addressService;
    private readonly IOrderService _orderService;
    private readonly IServiceService _serviceService;
    public PrivacyModel(ILogger<PrivacyModel> logger, IAddressService addressService, IOrderService orderService, IServiceService serviceService)
    {
        _logger = logger;
        _addressService = addressService;
        _orderService = orderService;
        _serviceService = serviceService;
    }

    public async Task OnGet()
    {
        await _addressService.Set(new AddressDto
        {
            Content = "Somewhere",
            PostalCode = "12345",
            AddressCategory = AddressCategory.Expert,
            CityId = 1,
        });

        await _serviceService.Set(new ServiceDto
        {
            Name = "Test Service",
            Description = "Test Service Description",
            IsAvailable = true,
            ParentServiceId = null,
            FileInfoId = null
        });
    }
}

