using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _03_Controller_To_View.Controllers
{
    public class HomeController : Controller
    {
        // Controllerdan View'e gitmek için action methodlar kullanýlýr.
        // IActionResult methodun geriye bir view yada bir view parçasý döndüreceðini belirtir.
        public IActionResult Index() // Bu method View/Home/Index.cshtml view'ini döndürecek
        {
            List<string> products = new List<string> { "Ürün 1", "Ürün2", "Ürün 3" };
            // veriyi Viewdata ile view'e gönderme
            ViewData["products"] = products;
            return View();
        }

        // Belirli ürünlerin detaylarýný göstermek için baþka bir action method
        public IActionResult Details(int id)
        {
            var product = $"Ürün {id} Detaylarý";
            ViewData["productDetail"] = product;
            return View();
        }

       
    }
}
