using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _05_ViewBag_ViewData_TempData.Controllers
{
    public class HomeController : Controller
    {
        /* 
            Controller'da ViewBag, ViewData ve TempData kullanýmý:
            1. ViewBag: Dinamik bir nesnedir ve verileri anahtar-deðer çiftleri olarak saklar. 
            Kullanýmý kolaydýr ancak derleme zamanýnda hata kontrolü yapýlmaz. 
            2. ViewData: Sözlük (dictionary) tabanlýdýr ve verileri anahtar-deðer çiftleri olarak saklar.
            3. TempData: Verileri bir istekten diðerine taþýmak için kullanýlýr. iki istek arasýnda veri saklar.
         
         
         */

        public IActionResult Index()
        {
            // ViewBag dinamik özellikler alýr ve bunun sayesinde herhangi bir türde veri saklayabiliriz.
            // Bu veriler sadece mevcut istek süresi boyunca (yani bir action method çaðrýsý sýrasýnda) geçerlidir.
            ViewBag.ad = "Tahsin";
            ViewBag.sonuc = true;
            ViewBag.yas = 30;
            List<string> renkler = new List<string> { "Kýrmýzý", "Mavi", "Yeþil" };
            ViewBag.liste = renkler;

            // ViewData, ViewBag'e benzer þekilde çalýþýr ancak sözlük tabanlýdýr. Veri 1 sonuç süresi boyunca geçerlidir.
            ViewData["ad"] = "Ayþe"; // key -value çiftleri þeklinde veri saklar
            ViewData["sonuc"] = false;
            ViewData["sayý"] = 30;

            // TempData, verileri bir istekten diðerine taþýmak için kullanýlýr.
            TempData["mesaj"] = "Ýþlem baþarýlý!";

            return View();
        }

        public IActionResult About()
        {
            TempData["message"] = TempData["mesaj"];
            return View();
        }


    }
}
