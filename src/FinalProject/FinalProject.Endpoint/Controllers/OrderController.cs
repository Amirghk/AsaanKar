using AutoMapper;
using FinalProject.Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Endpoint.Controllers
{
    [Authorize("IsCustomer")]
    public class OrderController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;
        private readonly IServiceService _serviceService;
        private readonly IOrderService _orderService;
        private readonly ILogger<HomeController> _logger;

        public OrderController(
            IMapper mapper,
            IUploadService uploadService,
            ILogger<HomeController> logger,
            IServiceService serviceService,
            IOrderService orderService)
        {
            _mapper = mapper;
            _uploadService = uploadService;
            _serviceService = serviceService;
            _orderService = orderService;
            _logger = logger;
        }
        public async Task<IActionResult> Set(int id)
        {
            var service = await _serviceService.GetById(id);
            return View(service);
        }
    }
}
