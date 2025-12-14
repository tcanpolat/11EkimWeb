using _19_EntityFrameworkExample.Data;
using _19_EntityFrameworkExample.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _19_EntityFrameworkExample.Controllers
{
    public class HomeController : Controller
    {    
        private readonly EducationContext _context;

        public HomeController(EducationContext context)
        {
            _context = context; 
        }
        public IActionResult Index()
        {
            return View();
        }

    }
}
