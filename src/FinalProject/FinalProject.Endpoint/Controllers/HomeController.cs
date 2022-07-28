using AutoMapper;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Endpoint.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Endpoint.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        private readonly IUploadService _uploadService;
        private readonly IServiceService _serviceService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            IMapper mapper,
            ICategoryService categoryService,
            IUploadService uploadService,
            ILogger<HomeController> logger,
            IServiceService serviceService)
        {
            _mapper = mapper;
            _categoryService = categoryService;
            _uploadService = uploadService;
            _serviceService = serviceService;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            _logger.LogTrace("Start of {method}", nameof(Index));
            var model = _mapper.Map<List<CategoryViewModel>>(await _categoryService.GetAll());

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Category(int id)
        {
            var categories = _mapper.Map<List<CategoryViewModel>>(await _categoryService.GetChildren(id));

            foreach (var category in categories)
            {
                category.Services = _mapper.Map<List<ServiceViewModel>>(await _serviceService.GetByCategoryId(category.Id));
            }
            return View(categories);
        }

    }
}
