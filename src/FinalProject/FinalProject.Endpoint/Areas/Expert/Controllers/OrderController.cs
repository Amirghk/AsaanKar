using AutoMapper;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Endpoint.Areas.Expert.Models;
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

        public OrderController(IExpertService expertService,
            IOrderService orderService,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _expertService = expertService;
            _orderService = orderService;
            _userManager = userManager;
            _mapper = mapper;
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
    }
}
