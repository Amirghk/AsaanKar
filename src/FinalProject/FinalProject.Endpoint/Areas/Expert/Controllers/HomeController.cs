using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Endpoint.Areas.Expert.Controllers
{
    [Area("Expert")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
