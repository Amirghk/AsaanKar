using FinalProject.Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProject.Endpoint.Areas.Identity.Pages.Account.Manage
{
    public class ChooseCityModel : PageModel
    {
        private readonly IProvinceService _provinceService;

        public ChooseCityModel(IProvinceService provinceService)
        {
            _provinceService = provinceService;
        }

        public IEnumerable<SelectListItem> Provinces { get; set; }

        public int AddressId = 0;

        public int State = 1;

        public async Task<IActionResult> OnGetAsync(CancellationToken cancellationToken)
        {
            var provinces = await _provinceService.GetAll(cancellationToken);
            Provinces = provinces.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.Id.ToString()
            });

            return Page();
        }

        public async Task<IActionResult> OnGetEditAsync(int id, CancellationToken cancellationToken)
        {
            var provinces = await _provinceService.GetAll(cancellationToken);
            Provinces = provinces.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.Id.ToString()
            });
            AddressId = id;
            State = 2;
            return Page();
        }
    }
}
