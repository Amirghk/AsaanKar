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
    //[Authorize("IsCustomer")]
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
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var model = _mapper.Map<List<OrderListViewModel>>(await _orderService.GetByUserId(user.Id));
            // TODO
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            // fill an orderSaveViewModel and send it back to db
            ViewBag.Addresses = await LoadAsync(user);
            var model = new OrderSaveViewModel()
            {
                ServiceId = id,
            };
            // get customer addresses

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderSaveViewModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation("Invalid input in {model}", model);
                return View(model);
            }
            var dto = _mapper.Map<OrderDto>(model);
            var user = await _userManager.GetUserAsync(User);
            dto.CustomerId = user.Id;
            await _orderService.Set(dto);


            return RedirectToAction(nameof(Index));

        }


        private async Task<List<AddressListViewModel>> LoadAsync(ApplicationUser user)
        {
            var customer = await _customerService.GetById(user.Id);
            var addresses = await _addressService.GetByUserId(user.Id);
            var model = new List<AddressListViewModel>();
            foreach (var address in addresses)
            {
                model.Add(new AddressListViewModel
                {
                    Id = address.Id,
                    Address = await _addressService.GetFullAddressToString(address),
                });
            }
            return model;
        }
    }
}
