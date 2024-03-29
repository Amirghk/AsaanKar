﻿using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            _logger.LogTrace("start of method : {methodName}", nameof(Index));
            var orders = _mapper.Map<List<OrderListViewModel>>(await _orderService.GetAll(cancellationToken));
            if (orders.Count == 0)
            {
                _logger.LogWarning("count of {orders} is 0", orders);
            }
            _logger.LogTrace("end of {method}", nameof(Index));
            return View(orders);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            _logger.LogTrace("start of method : {}", nameof(Delete));
            await _orderService.Remove(id, cancellationToken);
            _logger.LogTrace("end of method : {}", nameof(Delete));
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            _logger.LogTrace("start of method : {}", nameof(Edit));
            var model = _mapper.Map<OrderEditViewModel>(await _orderService.GetById(id, cancellationToken));
            _logger.LogTrace("end of method : {}", nameof(Edit));
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OrderEditViewModel model, CancellationToken cancellationToken)
        {
            _logger.LogTrace("start of method : {}", nameof(Edit));
            _logger.LogTrace("checking validity of : {}", model);
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "اطلاعات درست وارد کنید");
                return View(model);
            }
            _logger.LogTrace("calling {method} in service", "GetById");
            var record = await _orderService.GetById(model.Id, cancellationToken);
            record.State = model.State;
            record.DateCompleted = model.DateCompleted;
            _logger.LogTrace("calling {method} in service", "Update");
            await _orderService.Update(record, cancellationToken);
            _logger.LogTrace("end of method : {}", nameof(Edit));
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> EditBid(int id, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<BidViewModel>(await _bidService.GetById(id, cancellationToken));
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBid(BidViewModel model, CancellationToken cancellationToken)
        {
            var bid = _mapper.Map<BidDto>(model);
            await _bidService.Update(bid, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
        {
            var order = await _orderService.GetById(id, cancellationToken);
            var model = _mapper.Map<OrderListViewModel>(order);
            return View(model);
        }
    }
}



