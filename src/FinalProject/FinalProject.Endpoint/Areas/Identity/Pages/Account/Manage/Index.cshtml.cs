// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Domain.Enums;
using FinalProject.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Endpoint.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IExpertService _expertService;
        private readonly ICustomerService _customerService;
        private readonly IUploadService _uploadService;
        private readonly IWebHostEnvironment _environment;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IExpertService expertService,
            ICustomerService customerService,
            IUploadService uploadService,
            IWebHostEnvironment environment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _expertService = expertService;
            _customerService = customerService;
            _uploadService = uploadService;
            _environment = environment;
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
            public IFormFile ProfilePic { get; set; } = null;
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

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            if (Input.IsExpert) ViewData["IsExpert"] = true.ToString();
            else ViewData["IsExpert"] = false.ToString();
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }


            var uploadsRootFolder = Path.Combine(_environment.WebRootPath, "Uploads");
            var claims = User.Claims;
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
                    if (Input.ProfilePic != null && Input.ProfilePic.Length != 0)
                    {
                        await _uploadService.Set(new UploadServiceDto
                        {
                            FileCategory = FileCategory.ExpertProfilePic,
                            ExpertId = expert.Id,
                            FileName = Input.ProfilePic.FileName,
                            FileSize = Input.ProfilePic.Length,
                            UploadedFile = Input.ProfilePic,
                        }, uploadsRootFolder);
                    }
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


            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

    }
}
