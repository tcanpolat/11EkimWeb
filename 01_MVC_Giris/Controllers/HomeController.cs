using System.Diagnostics;
using _01_MVC_Giris.Models;
using Microsoft.AspNetCore.Mvc;

namespace _01_MVC_Giris.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet] // Bu attribute, bu metodun yalnýzca HTTP GET isteklerine yanýt vereceðini belirtir.
                  // [HttpPost] // Bu attribute, bu metodun yalnýzca HTTP POST isteklerine yanýt vereceðini belirtir.
        public IActionResult Index() // Methodun geri dönüþ tipi IActionResult
        {
            // IActionResult, ASP.NET Core MVC'de bir eylem metodunun (action method) geri dönüþ tipi olarak kullanýlýr.
            // Bu, eylem metodunun farklý türde sonuçlar döndürebilmesini saðlar. Örneðin, bir View (görünüm), a Redirect (yönlendirme) veya JSON verisi döndürebilir.

            // View / Home /Index.cshtml dosyasýný döndürür.

            return View();
        }

        public IActionResult About()
        {
            AboutModel model = new AboutModel();
            model.Title = "Hakkýmýzda Sayfasý";
            model.Description = "Bu web sitesi ASP.NET Core MVC kullanýlarak geliþtirilmiþtir.";
            return View(model);
        }
       
    }
}
