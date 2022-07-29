using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Endpoint.Models;
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

        public List<AddressListViewModel> Addresses { get; set; } = new();

        private async Task LoadAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            var customer = await _customerService.GetById(user.Id, cancellationToken);
            var addresses = await _addressService.GetByUserId(user.Id, cancellationToken);
            Addresses = new List<AddressListViewModel>();
            foreach (var address in addresses)
            {
                Addresses.Add(new AddressListViewModel
                {
                    Id = address.Id,
                    Address = await _addressService.GetFullAddressToString(address.Id, cancellationToken),
                });
            }
        }

        public async Task<IActionResult> OnGetAsync(CancellationToken cancellationToken)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user, cancellationToken);
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id, CancellationToken cancellationToken)
        {
            await _addressService.Remove(id, cancellationToken);
            return RedirectToPage(nameof(Index));
        }
    }
}
