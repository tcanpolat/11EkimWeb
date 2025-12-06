using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _08_CustomHelpers.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
