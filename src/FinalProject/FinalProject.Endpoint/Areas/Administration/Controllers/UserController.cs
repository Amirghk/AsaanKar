using AutoMapper;
using FinalProject.Application.Common.DataTransferObjects;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Endpoint.Areas.Administration.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Endpoint.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize("IsAdmin")]
    public class UserController : Controller
    {

        private readonly ICustomerService _customerService;
        private readonly IExpertService _expertService;
        private readonly IAddressService _addressService;
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;
        public UserController(
            ICustomerService customerService,
            IExpertService expertService,
            IMapper mapper,
            IAddressService addressService,
            ICityService cityService)
        {
            _customerService = customerService;
            _expertService = expertService;
            _addressService = addressService;
            _cityService = cityService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Customers = _mapper.Map<List<CustomerListVM>>(await _customerService.GetAll());
            ViewBag.Experts = _mapper.Map<List<ExpertListVM>>(await _expertService.GetAll());

            foreach (var expert in ViewBag.Experts)
            {
                List<AddressDto> expertAddress = await _addressService.GetByUserId(expert.Id);
                if (expertAddress.FirstOrDefault() != null)
                {
                    expert.City = (await _cityService.GetById(expertAddress.First().CityId)).Name;
                }
            }

            return View();
        }
    }
}
