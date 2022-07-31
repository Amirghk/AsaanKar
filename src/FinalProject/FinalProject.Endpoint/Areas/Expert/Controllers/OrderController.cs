using AutoMapper;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Endpoint.Areas.Expert.Models;
using FinalProject.Endpoint.Models;
using FinalProject.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Endpoint.Areas.Expert.Controllers
{
    [Area("Expert")]
    [Authorize("IsExpert")]
    public class OrderController : Controller
    {
        private readonly IExpertService _expertService;
        private readonly IOrderService _orderService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IBidService _bidService;

        public OrderController(IExpertService expertService,
            IOrderService orderService,
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            IBidService bidService)
        {
            _expertService = expertService;
            _orderService = orderService;
            _userManager = userManager;
            _mapper = mapper;
            _bidService = bidService;
        }
        public async Task<IActionResult> Available(CancellationToken cancellationToken)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var expert = await _expertService.GetById(user.Id, cancellationToken);
            if (expert.Address == null)
            {
                ViewBag.NoAddress = true;
                return View();
            }
            ViewBag.NoAddress = false;
            var model = _mapper.Map<List<ExpertOrderViewModel>>(await _orderService.GetAvailable(user.Id, cancellationToken));
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Bid(int orderId)
        {
            var model = new BidViewModel()
            {
                OrderId = orderId
            };
            return View(model);

        }

        public async Task<IActionResult> Bid(BidViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "اشتباه در وارد کردن اطلاعات!");
                return View(model);
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var orders = await _orderService.GetAvailable(user.Id, cancellationToken);
            // if order is not available to expert
            if (!orders.Select(x => x.Id).Contains(model.OrderId))
            {
                return Unauthorized();
            }
            model.ExpertId = user.Id;
            var dto = _mapper.Map<BidDto>(model);
            var id = await _bidService.Set(dto, cancellationToken);

            return RedirectToAction(nameof(Available));
        }

        public async Task<IActionResult> DeleteBid(int orderId, CancellationToken cancellationToken)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var order = await _orderService.GetById(orderId, cancellationToken);
            var bidId = order.Bids.Where(x => x.ExpertId == user.Id).SingleOrDefault().Id;
            await _bidService.Remove(bidId, cancellationToken);
            return RedirectToAction(nameof(Available));
        }
    }
}
