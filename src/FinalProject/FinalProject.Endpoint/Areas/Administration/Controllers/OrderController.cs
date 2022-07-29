using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Endpoint.Areas.Administration.Models;
using Microsoft.AspNetCore.Authorization;
using FinalProject.Endpoint.Models;
using FinalProject.Application.Common.DataTransferObjects;

namespace FinalProject.Endpoint.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize("IsAdmin")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderController> _logger;
        private readonly IBidService _bidService;

        public OrderController(
            IOrderService orderService,
            IMapper mapper,
            ILogger<OrderController> logger,
            IBidService bidService)
        {
            _orderService = orderService;
            _mapper = mapper;
            _logger = logger;
            _bidService = bidService;
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
                // TODO add errors to modelstate
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

        [HttpGet]
        public async Task<IActionResult> EditBid(int id)
        {
            var model = _mapper.Map<BidViewModel>(await _bidService.GetById(id));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditBid(BidViewModel model)
        {
            var bid = _mapper.Map<BidDto>(model);
            await _bidService.Update(bid);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var order = await _orderService.GetById(id);
            var model = _mapper.Map<OrderListViewModel>(order);
            return View(model);
        }
    }
}



