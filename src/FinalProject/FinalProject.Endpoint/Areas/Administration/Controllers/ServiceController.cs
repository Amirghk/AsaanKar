using AutoMapper;
using FinalProject.Application.Common.ConfigurationModels;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Endpoint.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;

namespace FinalProject.Endpoint.Areas.Administration.Controllers;

[Area("Administration")]
public class ServiceController : Controller
{

    private readonly IMapper _mapper;
    private readonly IServiceService _serviceService;
    private readonly AppSettings _appSettings;
    private readonly ICategoryService _categoryService;
    private readonly ILogger<CategoryController> _logger;
    private readonly string _rootPath;
    public ServiceController(
        ICategoryService categoryService,
        ILogger<CategoryController> logger,
        IMapper mapper,
        IWebHostEnvironment environment,
        IOptions<AppSettings> appSettings,
        IServiceService serviceService)
    {
        _categoryService = categoryService;
        _logger = logger;
        _mapper = mapper;
        _serviceService = serviceService;
        _appSettings = appSettings.Value;
        _rootPath = environment.WebRootPath;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var model = _mapper.Map<List<ServiceListViewModel>>(await _serviceService.GetAll(cancellationToken));
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        var categories = await _categoryService.GetAll(cancellationToken);
        // select categories that have no parent category
        ViewBag.Categories = categories.Where(x => x.ParentCategoryId != null).Select(x => new SelectListItem
        {
            Value = x.Id.ToString(),
            Text = x.Name,
        });
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ServiceSaveViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var uploadsRootFolder = Path.Combine(_rootPath, _appSettings.UploadsFolderName);

        var serviceDto = _mapper.Map<ServiceDto>(model);
        var serviceId = await _serviceService.Set(serviceDto, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _serviceService.Remove(id, cancellationToken);
        return RedirectToAction(nameof(Index));
    }
}
