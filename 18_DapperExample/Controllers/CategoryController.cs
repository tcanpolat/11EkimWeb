using Microsoft.AspNetCore.Mvc;

namespace _18_DapperExample.Controllers
{
    public class CategoryController : Controller
    {
        // category için tüm işlemler yapılacak
        public IActionResult Index()
        {
            return View();
        }
    }
}
