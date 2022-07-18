using FinalProject.Application.Common.Dtos;
using FinalProject.Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Endpoint.Areas.Administration.Pages.Users
{
    public class IndexModel : PageModel
    {
        public IEnumerable<AddressDto> Addresses { get; set; }
        private readonly IAddressService _service;
        public IndexModel(IAddressService service)
        {
            _service = service;
        }
        public async Task<IActionResult> OnGet()
        {
            Addresses = await _service.GetAll();
            return Page();
        }
    }
}
