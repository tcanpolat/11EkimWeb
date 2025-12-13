using _18_DapperExample.Data;
using Microsoft.AspNetCore.Mvc;

namespace _18_DapperExample.Controllers
{
    public class ProductController : Controller
    {
        private readonly DapperContext _context;

        public ProductController(DapperContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            using (var connection = _context.CreateConnection())
            {

            }
            return View();
        }
    }
}
