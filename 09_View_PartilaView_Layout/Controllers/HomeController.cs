using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _09_View_PartilaView_Layout.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Admin()
        {
            return View();
        }

       
    }
}
