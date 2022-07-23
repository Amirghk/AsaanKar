using AutoMapper;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Endpoint.Areas.Administration.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Endpoint.Areas.Administration.Pages.Orders
{
    [Authorize("IsAdmin")]
    public class IndexModel : PageModel
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public IndexModel(IOrderService orderService,
                          IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        public List<OrderListViewModel> Orders { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            Orders = _mapper.Map<List<OrderListViewModel>>(await _orderService.GetAll());
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _orderService.Remove(id);
            return RedirectToPage();
        }
    }
}
