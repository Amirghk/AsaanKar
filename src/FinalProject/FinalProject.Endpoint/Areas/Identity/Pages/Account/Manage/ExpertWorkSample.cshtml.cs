using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Domain.Enums;
using FinalProject.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Endpoint.Areas.Identity.Pages.Account.Manage
{
    public class ExpertWorkSampleModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUploadService _uploadService;
        private readonly string _rootPath;

        public ExpertWorkSampleModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUploadService uploadService,
            IWebHostEnvironment environment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _uploadService = uploadService;
            _rootPath = environment.WebRootPath;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel
        {
            public IFormFile WorkSample { get; set; } = null!;
        }
        public void OnGet()
        {
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
                ModelState.AddModelError("", "نه!");
                return Page();
            }


            var uploadsRootFolder = Path.Combine(_rootPath, "Uploads");

            if (Input.WorkSample != null && Input.WorkSample.Length != 0)
            {
                await _uploadService.SetExpertWorkSamples(new UploadServiceDto
                {
                    FileCategory = FileCategory.ExpertWorkSample,
                    ExpertId = user.Id,
                    FileName = Input.WorkSample.FileName,
                    FileSize = Input.WorkSample.Length,
                    UploadedFile = Input.WorkSample,
                }, uploadsRootFolder, cancellationToken);
            }


            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "نمونه کار افزوده شد!";
            return RedirectToPage();
        }
    }
}
