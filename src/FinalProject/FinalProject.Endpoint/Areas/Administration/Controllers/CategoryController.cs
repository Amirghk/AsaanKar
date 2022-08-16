using AutoMapper;
using FinalProject.Application.Common.ConfigurationModels;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Domain.Enums;
using FinalProject.Endpoint.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;

namespace FinalProject.Endpoint.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class CategoryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;
        private readonly AppSettings _appSettings;
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;
        private readonly string _rootPath;
        public CategoryController(
            ICategoryService categoryService,
            ILogger<CategoryController> logger,
            IMapper mapper,
            IWebHostEnvironment environment,
            IOptions<AppSettings> appSettings,
            IUploadService uploadService)
        {
            _categoryService = categoryService;
            _logger = logger;
            _mapper = mapper;
            _uploadService = uploadService;
            _appSettings = appSettings.Value;
            _rootPath = environment.WebRootPath;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var model = _mapper.Map<List<CategoryViewModel>>(await _categoryService.GetAll(cancellationToken));
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create(CancellationToken cancellationToken)
        {
            var categories = await _categoryService.GetAll(cancellationToken);
            // select categories that have no parent category
            ViewBag.Categories = categories.Where(x => x.ParentCategoryId == null).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name,
            });
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategorySaveViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _categoryService.GetAll(cancellationToken);
                // select categories that have no parent category
                ViewBag.Categories = categories.Where(x => x.ParentCategoryId == null).Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name,
                });
                return View(model);
            }

            var uploadsRootFolder = Path.Combine(_rootPath, _appSettings.UploadsFolderName);

            var categoryDto = _mapper.Map<CategoryDto>(model);
            var categoryId = await _categoryService.Set(categoryDto, cancellationToken);
            if (model.Picture != null && model.Picture.Length > 0)
            {
                await _uploadService.Set(new UploadServiceDto
                {
                    FileCategory = FileCategory.ServiceCategory,
                    CategoryId = categoryId,
                    FileName = model.Picture.FileName,
                    FileSize = model.Picture.Length,
                    UploadedFile = model.Picture,
                }, uploadsRootFolder, cancellationToken);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _categoryService.Remove(id, cancellationToken);
            return RedirectToAction(nameof(Index));
        }
    }
}
