using AutoMapper;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Endpoint.Areas.Administration.Models;
using FinalProject.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Endpoint.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize("IsAdmin")]
    public class UserController : Controller
    {

        private readonly ICustomerService _customerService;
        private readonly IExpertService _expertService;
        private readonly IAddressService _addressService;
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserController(
            ICustomerService customerService,
            IExpertService expertService,
            IMapper mapper,
            IAddressService addressService,
            ICityService cityService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _customerService = customerService;
            _expertService = expertService;
            _addressService = addressService;
            _cityService = cityService;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Customers = _mapper.Map<List<CustomerListVM>>(await _customerService.GetAll());
            ViewBag.Experts = _mapper.Map<List<ExpertListVM>>(await _expertService.GetAll());

            foreach (var expert in ViewBag.Experts)
            {
                List<AddressDto> expertAddress = await _addressService.GetByUserId(expert.Id);
                if (expertAddress.FirstOrDefault() != null)
                {
                    expert.City = (await _cityService.GetById(expertAddress.First().CityId)).Name;
                }
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Profile(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var model = await LoadAsync(user);
            if (model.IsExpert) ViewData["IsExpert"] = true.ToString();
            else ViewData["IsExpert"] = false.ToString();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            // TODO c token
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                // TODO
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await ChangeEmail(model);

            var isCustomer = await IsCustomer(user);

            if (isCustomer)
            {
                var customer = await _customerService.GetById(user.Id);
                var customerDto = new CustomerDto
                {
                    Id = customer.Id,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    BirthDate = model.BirthDate,
                    PhoneNumber = model.PhoneNumber,
                };
                await _customerService.Update(customerDto);
            }
            else
            {
                var expert = await _expertService.GetById(user.Id);
                var expertDto = new ExpertDto
                {
                    Id = expert.Id,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    BirthDate = model.BirthDate,
                    NationalCode = model.NationalCode,
                    PhoneNumber = model.PhoneNumber,
                };
                await _expertService.Update(expertDto);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string userName)
        {

            var user = await _userManager.FindByNameAsync(userName);
            // TODO null check
            var userId = user.Id;

            var isCustomer = await IsCustomer(user);

            var result = await _userManager.DeleteAsync(user);
            // if user was deleted successfuly from identity database 
            // softDelete users from our database
            if (result.Succeeded)
            {
                if (isCustomer)
                {
                    await _customerService.SoftDelete(userId);
                }
                else
                {
                    await _expertService.SoftDelete(userId);
                }
            }
            return LocalRedirect("/Administration/Users");
        }

        private async Task<bool> ChangeEmail(UserEditViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return false;
            }

            var email = await _userManager.GetEmailAsync(user);
            if (model.Email != email)
            {
                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateChangeEmailTokenAsync(user, model.Email);
                var result = await _userManager.ChangeEmailAsync(user, model.Email, code);
                if (!result.Succeeded)
                {
                    return false;
                }

                // In our UI email and user name are one and the same, so when we update the email
                // we need to update the user name.
                var setUserNameResult = await _userManager.SetUserNameAsync(user, model.Email);
                if (!setUserNameResult.Succeeded)
                {
                    return false;
                }

            }
            return true;
        }


        private async Task<UserEditViewModel> LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);

            var model = new UserEditViewModel();
            model.Username = userName;
            model.Email = await _userManager.GetEmailAsync(user);

            var isCustomer = await IsCustomer(user);

            if (isCustomer)
            {
                var customer = await _customerService.GetById(user.Id);

                model.FirstName = customer.FirstName;
                model.LastName = customer.LastName;
                model.BirthDate = customer.BirthDate;
                model.PhoneNumber = customer.PhoneNumber;
                model.IsCustomer = true;
                return model;
            }
            else
            {
                var expert = await _expertService.GetById(user.Id);

                model.FirstName = expert.FirstName;
                model.LastName = expert.LastName;
                model.BirthDate = expert.BirthDate;
                model.NationalCode = expert.NationalCode;
                model.PhoneNumber = expert.PhoneNumber;
                model.IsExpert = true;
                return model;
            }
        }

        private async Task<bool> IsCustomer(ApplicationUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);

            return claims.Select(x => x.Type).Contains("IsCustomer");

        }
    }
}
