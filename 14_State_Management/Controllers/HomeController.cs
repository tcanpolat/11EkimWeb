using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _14_State_Management.Controllers
{
    public class HomeController : Controller
    {
        // Session, Cookie
        /* 
             Session state management, kullanýcýya özel 
             verileri sunucu tarafýnda saklamak için kullanýlýr.Oturum sona erdiðinde 
             (örneðin, tarayýcý kapatýldýðýnda) bu veriler genellikle silinir.
             Session kullanýmý, kullanýcý kimlik doðrulamasý, alýþveriþ sepeti verileri
             gibi verileri saklamak için uygundur.
             SUNUCU TARAFINDA SAKLAR !
         
             Cookie, kullanýcýya ait verileri istemci tarafýnda (tarayýcýda) saklar.
             Cookie'ler, belirli bir süre boyunca veya tarayýcý kapatýldýðýnda saklanabilir.
             Geçerli süresi dolduðunda veya kullanýcý tarafýndan silindiðinde kaybolur.      
             Cookie ile kritik olmayan veriler saklanabilir. Sebebi 
             , kullanýcý tarafýnda (tarayýcý) saklandýðý için güvenlik riskleri olabilir.
             KULLANICI TARAFINDA SAKLAR !
         */
        public IActionResult Index()
        {
            // session olusturma
            // key-value seklinde veriler saklanir
            HttpContext.Session.SetString("KullaniciAdi", "AdminUser");


            // Cookie options
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(10), // Cookie'nin geçerlilik süresi
                HttpOnly = true, // Sadece HTTP isteklerinde eriþilebilir
                IsEssential = true // GDPR uyumluluðu için gereklidir
            };

            // Set Cookie
            // key - value seklinde veriler saklanir
            Response.Cookies.Append("KullaniciEmail", "tahsincanpolat@gmail.com");
            return View();
        }

        public IActionResult Privacy()
        {
            // session okuma
            var kullaniciAdi = HttpContext.Session.GetString("KullaniciAdi");
            ViewBag.KullaniciAdi = kullaniciAdi;

            // Cookie okuma
            var kullaniciEmail = Request.Cookies["KullaniciEmail"];
            ViewBag.KullaniciEmail = kullaniciEmail;
            return View();
        }

       
    }
}
