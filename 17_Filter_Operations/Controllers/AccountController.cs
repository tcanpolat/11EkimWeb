using Microsoft.AspNetCore.Mvc;

namespace _15_Filter_Operations.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
