using _15_Filter_Operations.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace _17_Filter_Operations.Controllers
{
    //[ServiceFilter(typeof(AuthorizationFilter))]
    public class HomeController : Controller
    {

        [ServiceFilter(typeof(ActionFilter))]
        public IActionResult Index()
        {
            return View();
        }

        //[ServiceFilter(typeof(AuthorizationFilter))]
        [ServiceFilter(typeof(ActionFilter))]
        public IActionResult Privacy()
        {
            return View();
        }

        [ServiceFilter(typeof(ExceptionFilter))]
        public IActionResult Result()
        {
            // Burada bir hata oluþturalým. Eðer hata oluþursa ExceptionFilter devreye girecek.
            throw new Exception("Bu bir test hatasýdýr.");
        }

        public IActionResult Page404()
        {
            return View();
        }

        
    }
}
