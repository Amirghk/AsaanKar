using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Endpoint.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IAddressService _addressService;

    public IndexModel(ILogger<IndexModel> logger, IAddressService addressService)
    {
        _logger = logger;
        _addressService = addressService;
    }

    public void OnGet()
    {
        // await _addressService.Set(new AddressDto
        // {
        //     Content = "Somewhere",
        //     Zip = 12345,
        //     AddressCategory = AddressCategory.Expert,
        //     CityId = 1,
        // });
    }
}
