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
            var orders = _mapper.Map<List<OrderListViewModel>>(await _orderService.GetAll());

            return View(orders);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _orderService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = _mapper.Map<OrderEditViewModel>(await _orderService.GetById(id));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OrderEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var record = await _orderService.GetById(model.Id);
            record.State = model.State;
            record.DateCompleted = model.DateCompleted;
            await _orderService.Update(record);
            return RedirectToAction(nameof(Index));

        }
    }
}



