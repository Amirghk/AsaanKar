using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Endpoint.Areas.Identity.Pages.Account.Manage
{
    public class CustomerAddressModel : PageModel
    {
        private readonly ICustomerService _customerService;
        private readonly ICityService _cityService;
        private readonly IProvinceService _provinceService;
        private readonly IAddressService _addressService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public CustomerAddressModel(
            ICustomerService customerService,
            ICityService cityService,
            IProvinceService provinceService,
            IAddressService addressService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _customerService = customerService;
            _cityService = cityService;
            _provinceService = provinceService;
            _addressService = addressService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public List<ListModel> Addresses { get; set; } = new();
        public class ListModel
        {
            public int Id { get; set; }
            public string Address { get; set; } = string.Empty;
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var customer = await _customerService.GetById(user.Id);
            var addresses = await _addressService.GetByUserId(user.Id);
            foreach (var address in addresses)
            {
                Addresses = new List<ListModel>();
                Addresses.Add(new ListModel
                {
                    Id = address.Id,
                    Address = await _addressService.GetFullAddressToString(address),
                });
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

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _addressService.Remove(id);
            return RedirectToPage(nameof(Index));
        }
    }
}
