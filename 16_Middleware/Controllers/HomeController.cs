using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _16_Middleware.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult AccessDenied()
        {
            ViewData["Message"] = "Üzgünüz, bu sayfaya eriþim yetkiniz bulunmamaktadýr.";
            // HTTP 403 Forbidden durum kodu ayarlamak iyi bir uygulamadýr.
            Response.StatusCode = 403;
            return View();
        }


    }
}
