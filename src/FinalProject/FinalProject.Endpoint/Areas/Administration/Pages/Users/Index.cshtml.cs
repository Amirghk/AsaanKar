using AutoMapper;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Endpoint.Areas.Administration.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Endpoint.Areas.Administration.Pages.Users
{
    [Authorize("IsAdmin")]
    public class IndexModel : PageModel
    {
        public IEnumerable<CustomerListVM> Customers { get; set; }
        public IEnumerable<ExpertListVM> Experts { get; set; }

        private readonly ICustomerService _customerService;
        private readonly IExpertService _expertService;
        private readonly IAddressService _addressService;
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;
        public IndexModel(ICustomerService customerService,
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
        public async Task<IActionResult> OnGetAsync()
        {
            Customers = _mapper.Map<List<CustomerListVM>>(await _customerService.GetAll());
            Experts = _mapper.Map<List<ExpertListVM>>(await _expertService.GetAll());

            foreach (var expert in Experts)
            {
                var expertAddress = (await _addressService.GetByUserId(expert.Id)).FirstOrDefault();
                if (expertAddress != null)
                {
                    expert.City = (await _cityService.GetById(expertAddress.CityId)).Name;
                }
            }
            return Page();
        }
    }
}
