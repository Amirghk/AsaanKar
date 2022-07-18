// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using FinalProject.Application.Common.Dtos;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Domain.Enums;
using FinalProject.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace FinalProject.Endpoint.Areas.Identity.Pages.Account
{
    public class CustomerRegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ILogger<CustomerRegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ICityService _cityService;
        private readonly IProvinceService _provinceService;
        private readonly ICustomerService _customerService;

        public CustomerRegisterModel(
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            ILogger<CustomerRegisterModel> logger,
            IEmailSender emailSender,
            ICityService cityService,
            IProvinceService provinceService,
            ICustomerService customerService)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _cityService = cityService;
            _provinceService = provinceService;
            _customerService = customerService;
        }


        [BindProperty]
        public CustomerInputModel Input { get; set; }

        public string ReturnUrl { get; set; }


        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public IEnumerable<SelectListItem> Cities { get; set; }
        public IEnumerable<SelectListItem> Provinces { get; set; }

        public class CustomerInputModel
        {

            [Required]
            [EmailAddress]
            [Display(Name = "ایمیل")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "رمزعبور")]
            public string Password { get; set; }


            [DataType(DataType.Password)]
            [Display(Name = "تایید رمزعبور")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [DataType(DataType.PhoneNumber)]
            [Display(Name = "شماره تلفن")]
            [StringLength(11, ErrorMessage = "The {0} must be {1} characters long.", MinimumLength = 11)]
            public string PhoneNumber { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "نام")]
            [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
            public string FirstName { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "نام خانوادگی")]
            [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
            public string LastName { get; set; }

            [DataType(DataType.Date)]
            [Display(Name = "تاریخ تولد")]
            public DateTime BirthDate { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    // Add related data to claims
                    var expertClaim = new Claim("IsCustomer", true.ToString());
                    await _userManager.AddClaimAsync(user, expertClaim);

                    var firstNameClaim = new Claim(ClaimTypes.GivenName, Input.FirstName);
                    await _userManager.AddClaimAsync(user, firstNameClaim);

                    var lastNameClaim = new Claim(ClaimTypes.Surname, Input.LastName);
                    await _userManager.AddClaimAsync(user, lastNameClaim);

                    var phoneNumberClaim = new Claim(ClaimTypes.MobilePhone, Input.PhoneNumber);
                    await _userManager.AddClaimAsync(user, phoneNumberClaim);

                    var birthDateClaim = new Claim(ClaimTypes.DateOfBirth, Input.BirthDate.ToString());
                    await _userManager.AddClaimAsync(user, birthDateClaim);




                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    // add related data to customer entity 
                    await _customerService.Set(new CustomerDto
                    {
                        Name = Input.FirstName + " " + Input.LastName,
                        Birthdate = Input.BirthDate,
                        CustomerId = userId
                    });

                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private ApplicationUser CreateUser()
        {

            return Activator.CreateInstance<ApplicationUser>();

        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}
