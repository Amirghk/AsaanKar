// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Endpoint.Areas.Identity.Pages.Account.Manage
{
    [Authorize("IsAdmin")]
    public class AdminIndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IExpertService _expertService;
        private readonly ICustomerService _customerService;

        public AdminIndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IExpertService expertService,
            ICustomerService customerService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _expertService = expertService;
            _customerService = customerService;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }


        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            [Display(Name = "نام")]
            public string FirstName { get; init; } = null!;
            [Display(Name = "نام خانوادگی")]
            public string LastName { get; set; } = null!;
            [Display(Name = "تاریخ تولد")]
            public DateTime? BirthDate { get; set; }
            [Display(Name = "کدملی")]
            public string NationalCode { get; set; } = String.Empty;
            public bool IsCustomer { get; set; } = false;
            public bool IsExpert { get; set; } = false;
        }


        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);


            Username = userName;

            var claims = await _userManager.GetClaimsAsync(user);
            foreach (var claim in claims)
            {
                if (claim.Type == "IsExpert")
                {
                    var expert = await _expertService.GetByUserId(user.Id);
                    Input = new InputModel
                    {
                        FirstName = expert.FirstName,
                        LastName = expert.LastName,
                        BirthDate = expert.BirthDate,
                        NationalCode = expert.NationalCode,
                        PhoneNumber = expert.PhoneNumber,
                        IsExpert = true
                    };

                    return;
                }
                if (claim.Type == "IsCustomer")
                {
                    var customer = await _customerService.GetByUserId(user.Id);
                    Input = new InputModel
                    {
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        BirthDate = customer.BirthDate,
                        PhoneNumber = customer.PhoneNumber,
                        IsCustomer = true
                    };
                    return;
                }
            }
        }

        // for admins
        // TODO : Needs authorization
        public async Task<IActionResult> OnGetAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            if (Input.IsExpert) ViewData["IsExpert"] = true.ToString();
            else ViewData["IsExpert"] = false.ToString();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }



            var claims = await _userManager.GetClaimsAsync(user);
            foreach (var claim in claims)
            {
                if (claim.Type == "IsExpert")
                {
                    var expert = await _expertService.GetByUserId(user.Id);
                    var expertDto = new ExpertDto
                    {
                        Id = expert.Id,
                        FirstName = Input.FirstName,
                        LastName = Input.LastName,
                        BirthDate = Input.BirthDate,
                        NationalCode = Input.NationalCode,
                        PhoneNumber = Input.PhoneNumber,
                        ExpertId = user.Id,
                    };
                    await _expertService.Update(expertDto);
                    break;
                }
                if (claim.Type == "IsCustomer")
                {
                    var customer = await _customerService.GetByUserId(user.Id);
                    var customerDto = new CustomerDto
                    {
                        Id = customer.Id,
                        FirstName = Input.FirstName,
                        LastName = Input.LastName,
                        BirthDate = Input.BirthDate,
                        PhoneNumber = Input.PhoneNumber,
                        CustomerId = user.Id,
                    };
                    await _customerService.Update(customerDto);
                    break;
                }
            }

            StatusMessage = "User profile has been updated";
            return LocalRedirect("/Administration/Users");
        }
        /// <summary>
        /// gets username as string and delets the user from identity database but the user entity in our database remains
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostDelete(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var userId = user.Id;
            var claims = await _userManager.GetClaimsAsync(user);
            bool isCustomer = false;
            bool isExpert = false;
            if (claims.Select(x => x.Type).Contains("IsCustomer"))
                isCustomer = true;
            else if (claims.Select(x => x.Type).Contains("IsExpert"))
                isExpert = true;
            var result = await _userManager.DeleteAsync(user);
            // if user was deleted successfuly from identity database 
            // softDelete users from our database
            if (result.Succeeded)
            {
                if (isExpert)
                {
                    await _expertService.SoftDelete(userId);
                }
                else if (isCustomer)
                {
                    await _customerService.SoftDelete(userId);
                }
            }
            return LocalRedirect("/Administration/Users");
        }
    }
}
