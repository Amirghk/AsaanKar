using AutoMapper;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Endpoint.Areas.Identity.Models;
using FinalProject.Endpoint.Common.Extensions;
using FinalProject.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProject.Endpoint.Areas.Identity.Pages.Account.Manage
{
    public class EditAddressModel : PageModel
    {
        private readonly IExpertService _expertService;
        private readonly ICityService _cityService;
        private readonly IProvinceService _provinceService;
        private readonly IAddressService _addressService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        public EditAddressModel(
            IExpertService expertService,
            ICityService cityService,
            IProvinceService provinceService,
            IAddressService addressService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IMapper mapper)
        {
            _expertService = expertService;
            _cityService = cityService;
            _provinceService = provinceService;
            _addressService = addressService;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        [BindProperty]
        public AddressViewModel Address { get; set; }

        public IEnumerable<SelectListItem> Cities { get; set; }
        public IEnumerable<SelectListItem> Provinces { get; set; }
        public int State = 1;


        public async Task LoadAsync(int id, CancellationToken cancellationToken)
        {
            Address = _mapper.Map<AddressViewModel>(await _addressService.GetById(id, cancellationToken));
        }

        public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
        {
            await LoadAsync(id, cancellationToken);
            var provinces = await _provinceService.GetAll(cancellationToken);
            Provinces = provinces.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.Id.ToString()
            });

            return Page();
        }

        public async Task<IActionResult> OnPostProvinceAsync(int id, CancellationToken cancellationToken)
        {
            State = 2;
            var cities = await _cityService.GetAll(cancellationToken);
            var provinceCities = cities.Where(c => c.ProvinceId == Address.ProvinceId).ToList();
            Cities = provinceCities.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name.ToString()
            });
            await LoadAsync(id, cancellationToken);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, CancellationToken cancellationToken)
        {
            var addressDto = await _addressService.GetById(id, cancellationToken);
            addressDto.CityId = Address.CityId;
            addressDto.Content = Address.Content;
            addressDto.PostalCode = Address.PostalCode;
            await _addressService.Update(addressDto, cancellationToken);
            if (User.IsExpert())
                return RedirectToPage("ExpertAddress");
            return RedirectToPage("CustomerAddress");
        }
    }
}
