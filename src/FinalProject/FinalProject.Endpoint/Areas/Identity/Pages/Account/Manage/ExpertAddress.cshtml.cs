using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Endpoint.Areas.Identity.Pages.Account.Manage
{
    public class ExpertAddressModel : PageModel
    {
        private readonly IExpertService _expertService;
        private readonly ICityService _cityService;
        private readonly IProvinceService _provinceService;
        private readonly IAddressService _addressService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ExpertAddressModel(
            IExpertService expertService,
            ICityService cityService,
            IProvinceService provinceService,
            IAddressService addressService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _expertService = expertService;
            _cityService = cityService;
            _provinceService = provinceService;
            _addressService = addressService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Address { get; set; } = string.Empty;
        public int AddressId { get; set; }
        private async Task LoadAsync(ApplicationUser user)
        {
            var expert = await _expertService.GetById(user.Id);
            if (expert.Address != null)
            {
                Address = await _addressService.GetFullAddressToString(expert.Address);
                AddressId = expert.Address.Id;
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }
    }
}
