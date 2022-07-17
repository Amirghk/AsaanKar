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
    private readonly IProvinceService _provinceService;
    private readonly ICityService _cityService;
    public PrivacyModel(ILogger<PrivacyModel> logger, IAddressService addressService, IProvinceService provinceService, ICityService cityService)
    {
        _logger = logger;
        _addressService = addressService;
        _provinceService = provinceService;
        _cityService = cityService;
    }

    public async Task OnGet()
    {
        await _provinceService.Set(new ProvinceDto
        {
            Name = "Tehran",
            IsSupported = true,
        });
        await _cityService.Set(new CityDto
        {
            Name = "Tehran",
            ProvinceId = 1,
            IsSupported = false,
        });
        await _addressService.Set(new AddressDto
        {
            Content = "Somewhere",
            PostalCode = "12345",
            AddressCategory = AddressCategory.Expert,
            CityId = 1,
        });

        //await _serviceService.Set(new ServiceDto
        //{
        //    Name = "Test Service",
        //    Description = "Test Service Description",
        //    IsAvailable = true,
        //    ParentServiceId = null,
        //    FileInfoId = null
        //});
    }
}

