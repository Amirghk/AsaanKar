using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Endpoint.Areas.Administration.Controllers
{
    public class HomeController : Controller
    {
        [Route("Administration/Home/Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
