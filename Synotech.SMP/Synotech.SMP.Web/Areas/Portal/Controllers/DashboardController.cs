using Microsoft.AspNetCore.Mvc;

namespace Synotech.SMP.Web.Areas.Portal.Controllers
{
    [Area("Portal")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
