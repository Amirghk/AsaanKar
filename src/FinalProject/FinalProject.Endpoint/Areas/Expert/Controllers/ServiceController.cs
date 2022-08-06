using FinalProject.Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FinalProject.Infrastructure.Identity;
using AutoMapper;
using FinalProject.Endpoint.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProject.Endpoint.Areas.Expert.Controllers
{
    [Area("Expert")]
    [Authorize("IsExpert")]
    public class ServiceController : Controller
    {
        private readonly ILogger<ServiceController> _logger;
        private readonly IServiceService _serviceService;
        private readonly ICategoryService _categoryService;
        private readonly IExpertService _expertService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public ServiceController(
            ILogger<ServiceController> logger,
            IServiceService serviceService,
            ICategoryService categoryService,
            IExpertService expertService,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _logger = logger;
            _serviceService = serviceService;
            _categoryService = categoryService;
            _expertService = expertService;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var services = _mapper.Map<List<ServiceViewModel>>(await _serviceService.GetAll(cancellationToken, expertId: user.Id));
            return View(services);
        }

        [HttpGet]
        public async Task<IActionResult> Add(CancellationToken cancellationToken)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var expertServices = _mapper.Map<List<ServiceViewModel>>(await _serviceService.GetAll(cancellationToken, expertId: user.Id));
            var services = _mapper.Map<List<ServiceViewModel>>(await _serviceService.GetAll(cancellationToken));
            var servicesNotAdded = services.Where(x => !(expertServices.Select(x => x.Id).Contains(x.Id))).ToList();
            ViewBag.Services = servicesNotAdded.Select(x => new SelectListItem
            {
                Text = x.Description,
                Value = x.Id.ToString()
            });
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(List<int> services, CancellationToken cancellationToken)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            await _expertService.AddServices(user.Id, services, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            await _expertService.RemoveService(user.Id, id, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Error(string type)
        {
            if (type == null)
            {
                ViewBag.Message = "Error";
            }
            if (type == "unfinished")
            {
                ViewBag.Message = "سفارش مرتبط با این سرویس تمام نشده";
            }
            return View();
        }
    }
}
