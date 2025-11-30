using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _02_Routes.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult AboutDetail(int id) // URL'den gelen 'id' parametresini alýr about/detail/5 gibi
        {
            ViewBag.Id = id;
            return View();
        }

    }
}
