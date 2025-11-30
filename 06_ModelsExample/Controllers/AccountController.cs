using _06_ModelsExample.Models;
using Microsoft.AspNetCore.Mvc;

namespace _06_ModelsExample.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            User user = new User
            {
                Name = "Tahsin",
                Surname = "Canpolat",
                Age = 33
            };

            ViewBag.User = user; // ViewBag ile modeli view'a gönderiyoruz

            return View(user); // Modeli doğrudan view'a gönderiyoruz
        }

        public IActionResult ProductPage()
        {
            Product product1 = new Product
            {
                Id = 1,
                Name = "Laptop",
                Description = "Yüksek performanslı laptop",
                Price = 1500.00m
            };

            Product product2 = new Product
            {
                Id = 2,
                Name = "Akıllı Telefon",
                Description = "Son model akıllı telefon",
                Price = 800.00m
            };

            List<Product> products = new List<Product>();
            products.Add(product1);
            products.Add(product2);

            ViewBag.Products = products; // ViewBag ile modeli view'a gönderiyoruz

            return View();
        }
    }
}
