using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Endpoint.Areas.Administration.Models;
using Microsoft.AspNetCore.Authorization;

namespace FinalProject.Endpoint.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize("IsAdmin")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderController> _logger;

        public OrderController(
            IOrderService orderService,
            IMapper mapper,
            ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogTrace("start of method : {methodName}", nameof(Index));
            var orders = _mapper.Map<List<OrderListViewModel>>(await _orderService.GetAll());
            if (orders.Count == 0)
            {
                _logger.LogWarning("count of {orders} is 0", orders);
            }
            _logger.LogTrace("end of {method}", nameof(Index));
            return View(orders);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogTrace("start of method : {}", nameof(Delete));
            await _orderService.Remove(id);
            _logger.LogTrace("end of method : {}", nameof(Delete));
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            _logger.LogTrace("start of method : {}", nameof(Edit));
            var model = _mapper.Map<OrderEditViewModel>(await _orderService.GetById(id));
            _logger.LogTrace("end of method : {}", nameof(Edit));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OrderEditViewModel model)
        {
            _logger.LogTrace("start of method : {}", nameof(Edit));
            _logger.LogTrace("checking validity of : {}", model);
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _logger.LogTrace("calling {method} in service", "GetById");
            var record = await _orderService.GetById(model.Id);
            record.State = model.State;
            record.DateCompleted = model.DateCompleted;
            _logger.LogTrace("calling {method} in service", "Update");
            await _orderService.Update(record);
            _logger.LogTrace("end of method : {}", nameof(Edit));
            return RedirectToAction(nameof(Index));

        }
    }
}



