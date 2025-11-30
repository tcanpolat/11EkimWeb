using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace _07_HtmlHelper_Models.Models
{
    public class User
    {
        // Userın instance'ı alındığında propertylerin durumunu kontrol altına alabiliyoruz.
        // Zorunlu alanlar, belirli aralıklarda olsun ya da hata oluşursa nasıl bir mesaj gözüksün gibi.
        // Bu kontrolleri attribute'lar ile yapıyoruz. DataAnnotations namespace'i altında yer alıyorlar.

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }  
        [Required(ErrorMessage = "Age is required")]
        [Range(1, 120, ErrorMessage = "Age must be between 1 and 120")]
        public int Age { get; set; }
        public bool IsSubscribed { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }

        public List<SelectListItem> CountryList { get; set; } = new List<SelectListItem>();
    }
}
