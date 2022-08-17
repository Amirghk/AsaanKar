using FinalProject.Application.Common.ConfigurationModels;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Domain.Enums;
using FinalProject.Endpoint.Areas.Expert.Models;
using FinalProject.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FinalProject.Endpoint.Areas.Expert.Controllers
{
    [Area("Expert")]
    [Authorize("IsExpert")]
    public class WorkSampleController : Controller
    {
        private readonly IUploadService _uploadService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppSettings _appSettings;
        private string _rootPath;

        public WorkSampleController(
            IUploadService uploadService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment,
            IOptions<AppSettings> appSettings)
        {
            _uploadService = uploadService;
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _rootPath = environment.WebRootPath;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var samples = await _uploadService.GetAll(cancellationToken, expertId: user.Id, fileCategory: FileCategory.ExpertWorkSample);
            var model = samples.Select(x => x.Id).ToList();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ExpertWorkSampleViewModel model, CancellationToken cancellationToken)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "نه!");
                return View();
            }


            var uploadsRootFolder = Path.Combine(_rootPath, "Uploads");

            if (model.WorkSample != null && model.WorkSample.Length != 0)
            {
                await _uploadService.SetExpertWorkSamples(new UploadServiceDto
                {
                    FileCategory = FileCategory.ExpertWorkSample,
                    ExpertId = user.Id,
                    FileName = model.WorkSample.FileName,
                    FileSize = model.WorkSample.Length,
                    UploadedFile = model.WorkSample,
                }, uploadsRootFolder, cancellationToken);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var uploadsPath = Path.Combine(_rootPath, _appSettings.UploadsFolderName);
            await _uploadService.Remove(id, uploadsPath, cancellationToken);
            return RedirectToAction(nameof(Index));
        }
    }
}
