using AutoMapper;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FinalProject.Endpoint.Models;
using FinalProject.Application.Common.DataTransferObjects;

namespace FinalProject.Endpoint.Controllers
{
    [Authorize("IsCustomer")]
    public class OrderController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;
        private readonly IAddressService _addressService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<HomeController> _logger;

        public OrderController(
            IMapper mapper,
            IUploadService uploadService,
            ILogger<HomeController> logger,
            IOrderService orderService,
            ICustomerService customerService,
            IAddressService addressService,
            UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _uploadService = uploadService;
            _orderService = orderService;
            _customerService = customerService;
            _addressService = addressService;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var model = _mapper.Map<List<OrderListViewModel>>(await _orderService.GetByUserId(user.Id, cancellationToken));
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int id, CancellationToken cancellationToken)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            ViewBag.Addresses = await LoadAsync(user, cancellationToken);
            ViewBag.ServiceId = id;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderSaveViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "اطلاعات درست وارد کنید");
                _logger.LogInformation("Invalid input in {model}", model);
                return View(model);
            }
            var dto = _mapper.Map<OrderDto>(model);
            var user = await _userManager.GetUserAsync(User);
            dto.CustomerId = user.Id;
            await _orderService.Set(dto, cancellationToken);

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
        {
            var order = await _orderService.GetById(id, cancellationToken);
            var model = _mapper.Map<OrderListViewModel>(order);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveBid(int bidId, int orderId, CancellationToken cancellationToken)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var order = await _orderService.GetById(orderId, cancellationToken);
            if (order.CustomerId != user.Id)
            {
                throw new InvalidOperationException("You can only accept bid on your own orders!");
            }
            await _orderService.ApproveBid(orderId, bidId, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        private async Task<List<AddressListViewModel>> LoadAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            var customer = await _customerService.GetById(user.Id, cancellationToken);
            var addresses = await _addressService.GetByUserId(user.Id, cancellationToken);
            var model = new List<AddressListViewModel>();
            foreach (var address in addresses)
            {
                model.Add(new AddressListViewModel
                {
                    Id = address.Id,
                    Address = await _addressService.GetFullAddressToString(address.Id, cancellationToken),
                });
            }
            return model;
        }
    }
}
