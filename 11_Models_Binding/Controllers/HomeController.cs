using _11_Models_Binding.Models;
using _11_Models_Binding.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _11_Models_Binding.Controllers
{
    public class HomeController : Controller
    {
       

        public IActionResult Index()
        {
            Kisi kisi = new Kisi
            {
                Ad = "Ahmet",
                Soyad = "Yýlmaz",
                Yas = 30
            };
            return View(kisi);
        }

        [HttpPost]
        public IActionResult Index(Kisi kisi)
        {
            return View(kisi);
        }
        public IActionResult HomePage()
        {
            // Hem Kisi Hemde Adres modelini göndermek istiyoruz.
            Kisi kisi = new Kisi
            {
                Ad = "Mehmet",
                Soyad = "Demir",
                Yas = 25
            };

            Adres adres = new Adres
            {
                Sehir = "Ýstanbul",
                AdresTanim = "Kadýköy, Ýstanbul"
            };
            // ViewModel kullanarak birden fazla modeli View'a gönderebiliriz.
            // View de gösterilecek model tipi ViewModel olacak.
            // Models binding (Modelleri birleþtirdi) iþlemi de ViewModel üzerinden yapýlacak.
            KisiAdres homePageViewModel = new KisiAdres
            {
                Kisi = kisi,
                Adres = adres
            };

            return View(homePageViewModel);
        }

       
    }
}
