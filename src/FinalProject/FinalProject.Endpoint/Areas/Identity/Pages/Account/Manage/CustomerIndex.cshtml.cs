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
using FinalProject.Endpoint.Common.Validations;
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
        private readonly string _rootPath;

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
            _rootPath = environment.WebRootPath;
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
            [DataType(DataType.Upload)]
            [UploadFileExtentions(".png,.jpg,.jpeg,.gif")]
            public IFormFile? ProfilePic { get; set; }
            public string ProfilePicAddress { get; set; } = String.Empty;
        }


        private async Task LoadAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            var userName = await _userManager.GetUserNameAsync(user);


            Username = userName;
            string profilePicAddress = string.Empty;


            var customer = await _customerService.GetById(user.Id, cancellationToken);
            // get customer profile pic
            if (customer.ProfilePictureId != null)
            {
                try
                {
                    profilePicAddress = await _uploadService.GetFileDirectory((int)customer.ProfilePictureId, cancellationToken);
                }
                catch (Exception)
                {
                    profilePicAddress = String.Empty;
                }
            }

            Input = new InputModel
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                BirthDate = customer.BirthDate,
                PhoneNumber = customer.PhoneNumber,
                ProfilePicAddress = profilePicAddress
            };

            return;
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


        public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user, cancellationToken);
                return Page();
            }


            var uploadsRootFolder = Path.Combine(_rootPath, "Uploads");

            var customer = await _customerService.GetById(user.Id, cancellationToken);
            var customerDto = new CustomerDto
            {
                Id = customer.Id,
                FirstName = Input.FirstName,
                LastName = Input.LastName,
                BirthDate = Input.BirthDate,
                PhoneNumber = Input.PhoneNumber,
            };
            await _customerService.Update(customerDto, cancellationToken);

            if (Input.ProfilePic != null && Input.ProfilePic.Length != 0)
            {
                await _uploadService.Set(new UploadServiceDto
                {
                    FileCategory = FileCategory.Customer,
                    CustomerId = customer.Id,
                    FileName = Input.ProfilePic.FileName,
                    FileSize = Input.ProfilePic.Length,
                    UploadedFile = Input.ProfilePic,
                }, uploadsRootFolder, cancellationToken);
            }



            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

    }
}
