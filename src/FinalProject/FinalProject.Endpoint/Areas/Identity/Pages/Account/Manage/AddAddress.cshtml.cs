using AutoMapper;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Domain.Enums;
using FinalProject.Endpoint.Areas.Identity.Models;
using FinalProject.Endpoint.Common.Extensions;
using FinalProject.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProject.Endpoint.Areas.Identity.Pages.Account.Manage
{
    public class AddAddressModel : PageModel
    {
        private readonly IExpertService _expertService;
        private readonly ICityService _cityService;
        private readonly IProvinceService _provinceService;
        private readonly IAddressService _addressService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        public AddAddressModel(
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
        //public IEnumerable<SelectListItem> Provinces { get; set; }




        //public async Task<IActionResult> OnGetAsync(CancellationToken cancellationToken)
        //{
        //    var provinces = await _provinceService.GetAll(cancellationToken);
        //    Provinces = provinces.Select(p => new SelectListItem
        //    {
        //        Text = p.Name,
        //        Value = p.Id.ToString()
        //    });

        //    return Page();
        //}

        public async Task<IActionResult> OnGetAsync(int provinceId, CancellationToken cancellationToken)
        {
            var cities = await _cityService.GetAll(cancellationToken);
            var provinceCities = cities.Where(c => c.ProvinceId == provinceId).ToList();
            Cities = provinceCities.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name.ToString()
            });

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var record = new AddressDto
            {
                CityId = Address.CityId,
                Content = Address.Content,
                PostalCode = Address.PostalCode,
            };
            if (User.IsExpert())
            {
                record.ExpertId = user.Id;
                record.AddressCategory = AddressCategory.Expert;
            }
            if (User.IsCustomer())
            {
                record.CustomerId = user.Id;
                record.AddressCategory = AddressCategory.Customer;
            }
            await _addressService.Set(record, cancellationToken);
            if (User.IsExpert())
                return RedirectToPage("ExpertAddress");
            return RedirectToPage("CustomerAddress");
        }
    }
}
