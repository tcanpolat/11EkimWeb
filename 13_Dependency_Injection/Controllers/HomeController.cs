using _13_Dependency_Injection.Models;
using _13_Dependency_Injection.Services.Abstract;
using _13_Dependency_Injection.Services.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _13_Dependency_Injection.Controllers
{
    public class HomeController : Controller
    {
        // Dependency Injection ile servisleri controller'a enjekte ediyoruz.
        private readonly IMyService _myService;

        public HomeController(IMyService myService)
        {
            _myService = myService;
        }
        public IActionResult Index()
        {
            List<Student> students = _myService.GetStudents();
            return View(students);
        }

        public IActionResult Details() { 
            var students = _myService.GetStudents();
            return View(students);
        }

        
    }
}
