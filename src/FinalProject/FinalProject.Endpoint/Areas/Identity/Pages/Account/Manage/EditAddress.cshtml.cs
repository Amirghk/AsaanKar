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

        public async Task LoadAsync(int id, CancellationToken cancellationToken)
        {
            Address = _mapper.Map<AddressViewModel>(await _addressService.GetById(id, cancellationToken));
        }


        public async Task<IActionResult> OnGetAsync(int addressId, int provinceId, CancellationToken cancellationToken)
        {


            var cities = await _cityService.GetAll(cancellationToken);
            var provinceCities = cities.Where(c => c.ProvinceId == provinceId).ToList();
            Cities = provinceCities.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name.ToString()
            });

            await CheckIfAddressBelongsToUser(addressId, cancellationToken);
            await LoadAsync(addressId, cancellationToken);


            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, CancellationToken cancellationToken)
        {
            await CheckIfAddressBelongsToUser(id, cancellationToken);
            var addressDto = await _addressService.GetById(id, cancellationToken);
            addressDto.CityId = Address.CityId;
            addressDto.Content = Address.Content;
            addressDto.PostalCode = Address.PostalCode;
            await _addressService.Update(addressDto, cancellationToken);
            if (User.IsExpert())
                return RedirectToPage("ExpertAddress");
            return RedirectToPage("CustomerAddress");
        }

        public async Task CheckIfAddressBelongsToUser(int addressId, CancellationToken cancellationToken)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new UnauthorizedAccessException("User not logged in with ID '{_userManager.GetUserId(User)}");
            }
            bool result;

            if (User.IsExpert())
            {
                result = await _addressService.CheckIfAddressIsForUser(addressId, user.Id, AddressCategory.Expert, cancellationToken);
            }
            else
            {
                result = await _addressService.CheckIfAddressIsForUser(addressId, user.Id, AddressCategory.Customer, cancellationToken);
            }
            if (!result)
            {
                throw new UnauthorizedAccessException($"User {user.Id} tried to access Address {addressId}");
            }
        }
    }
}
