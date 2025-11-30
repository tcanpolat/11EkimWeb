using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _04_View_To_Controller.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet] // Bu attribute, bu metodun yalnýzca HTTP GET isteklerine yanýt vereceðini belirtir.
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost] // Bu attribute, bu metodun yalnýzca HTTP POST isteklerine yanýt vereceðini belirtir.
        public IActionResult KisiGonder(string ad,string kisiler,bool onay)
        {
            return Redirect("Index"); // Redirect ile baþka bir action'a yönlendirme yapýlabilir.
        }

        
    }
}
