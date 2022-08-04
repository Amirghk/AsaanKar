using AutoMapper;
using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Endpoint.Areas.Expert.Models;
using FinalProject.Endpoint.Models;
using FinalProject.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Endpoint.Areas.Expert.Controllers
{
    [Area("Expert")]
    public class HomeController : Controller
    {
        private readonly IExpertService _expertService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public HomeController(
            IExpertService expertService,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _expertService = expertService;
            _userManager = userManager;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> PublicProfile(string id, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<ExpertPublicProfileViewModel>(await _expertService.GetExpertPublicProfile(id, cancellationToken));

            return View(model);
        }
    }
}
