using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Domain.Enums;
using FinalProject.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Endpoint.Areas.Identity.Pages.Account.Manage;

public class ExpertIndexModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IExpertService _expertService;
    private readonly ICustomerService _customerService;
    private readonly IUploadService _uploadService;
    private readonly string _rootPath;

    public ExpertIndexModel(
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
        [Display(Name = "کدملی")]
        public string NationalCode { get; set; } = String.Empty;
        public IFormFile ProfilePic { get; set; }
        public string ProfilePicAddress { get; set; } = String.Empty;
    }


    private async Task LoadAsync(ApplicationUser user)
    {
        var userName = await _userManager.GetUserNameAsync(user);


        Username = userName;
        string profilePicAddress = string.Empty;


        var expert = await _expertService.GetById(user.Id);
        // get expert profile pic
        if (expert.ProfilePictureId != null)
        {
            try
            {
                profilePicAddress = await _uploadService.GetFileDirectory((int)expert.ProfilePictureId);
            }
            catch (Exception)
            {
                profilePicAddress = String.Empty;
            }
        }


        Input = new InputModel
        {
            FirstName = expert.FirstName,
            LastName = expert.LastName,
            BirthDate = expert.BirthDate,
            NationalCode = expert.NationalCode,
            PhoneNumber = expert.PhoneNumber,
            ProfilePicAddress = profilePicAddress
        };

        return;
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


        var uploadsRootFolder = Path.Combine(_rootPath, "Uploads");

        var expert = await _expertService.GetById(user.Id);
        var expertDto = new ExpertDto
        {
            Id = expert.Id,
            FirstName = Input.FirstName,
            LastName = Input.LastName,
            BirthDate = Input.BirthDate,
            NationalCode = Input.NationalCode,
            PhoneNumber = Input.PhoneNumber,
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


        await _signInManager.RefreshSignInAsync(user);
        StatusMessage = "Your profile has been updated";
        return RedirectToPage();
    }
}

