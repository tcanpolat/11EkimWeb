using _07_HtmlHelper_Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace _07_HtmlHelper_Models.Controllers
{
    public class HomeController : Controller
    {
       
        public IActionResult Index()
        {
            User model = new User()
            {
                CountryList = GetCountries()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Submit(User model)
        {
            User user = new User()
            {
                CountryList = GetCountries()
            };

            if (ModelState.IsValid) // Modelin geçerliliðini kontrol et
            {
                ViewBag.Message = $"Merhaba {model.Name}, kaydýnýz baþarýyla alýndý!";
                return View("Result",model);
            }
            return View("Index", user);
        }

        public List<SelectListItem> GetCountries()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "United States", Value = "US" },
                new SelectListItem { Text = "Canada", Value = "CA" },
                new SelectListItem { Text = "United Kingdom", Value = "UK" },
                new SelectListItem { Text = "Australia", Value = "AU" },
                new SelectListItem { Text = "Germany", Value = "DE" }
            };
        }
    }
}
