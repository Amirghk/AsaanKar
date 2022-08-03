using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Endpoint.Areas.Administration.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
