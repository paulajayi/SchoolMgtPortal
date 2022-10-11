using Microsoft.AspNetCore.Mvc;

namespace Synotech.SMP.Web.Areas.Portal.Controllers
{
    [Area("Portal")]
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
           return RedirectToAction("Login", "Home");           
        }


       

        public IActionResult Login()
        {
            return View();
        }




        public IActionResult About()
        {
            return View();
        }
    }
}
