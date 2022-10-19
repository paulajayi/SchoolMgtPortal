using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;

namespace Synotech.SMP.Web.Areas.Portal.Controllers
{
    [DefaultBreadcrumb]
    [Area("Portal")]
    public class DashboardController : Controller
    {
        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "Dashboard";
            return View();
        }
    }
}
