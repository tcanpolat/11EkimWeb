using _19_EntityFrameworkExample.Data;
using _19_EntityFrameworkExample.Extensions;
using _19_EntityFrameworkExample.Models;
using _19_EntityFrameworkExample.ViewModels;
using EFCore.BulkExtensions;
using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using System.Diagnostics;

namespace _19_EntityFrameworkExample.Controllers
{
    public class HomeController : Controller
    {    
        private readonly EducationContext _context;

        // Dependency Injection ile DbContext alýnýr
        public HomeController(EducationContext context)
        {
            _context = context; 
        }
        public IActionResult Index()
        {
            // Linq sorgularý entity framework ile veritabaný arasýndaki sql sorgularýný oluþturur
            List<Student> students = _context.Students.ToList(); // Öðrencileri veritabanýndan alýr
            return View(students);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // CSRF korumasý için Csrf => Cross-Site Request Forgery
        public IActionResult Create([Bind("Id,Name,Age,Department")] Student student)
        {
            if (ModelState.IsValid)
            {
                // model geçerliyse öðrenciyi veritabanýna ekle
                _context.Add(student);
                _context.SaveChanges(); // veritabanýna kaydet
                return RedirectToAction("Index");
            }

            return View(student);
        }

        public IActionResult Details(int id)
        {
            // Db den id 'ye göre öðrenci bul
            Student student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);

        }

        public IActionResult Edit(int id)
        {
            var student = _context.Students.Find(id);
            if(student is null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Age,Department")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                // model geçerliyse öðrenciyi güncelle
                try
                {
                    _context.Update(student);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException err)
                {
                    // Güncelleme sýrasýnda bir hata oluþtuysa, öðrencinin hala var olup olmadýðýný kontrol et
                    if (!StudentExists(student.Id))
                    {
                        return NotFound();

                    }
                    else
                    {
                        throw; // Hata fýrlat
                    }
                    
                }
               
                return RedirectToAction("Index");
            }
            return View(student);
        }
        public bool StudentExists(int id)
        {
            // Any metodu ile belirtilen id'ye sahip öðrenci var mý kontrol et. Geriye true/false döner
            return _context.Students.Any(e => e.Id == id);
        }

        // Belirli bir öðrenciye týkladýðýmda silme onay sayfasýný gösterir
        public IActionResult Delete(int id)
        {
            var student = _context.Students.Find(id);
            if(student is null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Öðrenciyi bul
            var student = _context.Students.Find(id);
            if(student is null)
            {
                return NotFound();
            }
            // bulduðun öðrenciyi sil
            _context.Students.Remove(student);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        
        public IActionResult QuerySyntax()
        {
            // LINQ Query Syntax kullanarak 18 yaþýndan büyük öðrencileri al
            // Query Syntax, SQL'e benzer bir sözdizimi kullanýr
            var students = (from s in _context.Students
                             where s.Age > 18
                             select s).ToList();
            return View("Index", students);
        }

        public IActionResult MethodSyntax()
        {
            // LINQ Method Syntax kullanarak 18 yaþýndan küçük öðrencileri al
            var students = _context.Students
                                .Where(s => s.Age < 18)
                                .ToList();

            return View("Index", students);
        }

        public IActionResult Join()
        {
            // Öðrenciler ve kurslar arasýnda join iþlemi yaparak her öðrencinin aldýðý kurslarý listele
            //var studentCourses = from s in _context.Students
            //                     join c in _context.Courses
            //                     on s.Id equals c.StudentId
            //                     select new
            //                     {
            //                         StudentName = s.Name,
            //                         CourseTitle = c.Title
            //                     };

            // Method Syntax ile de yapýlabilir
           var studentCourses = _context.Students
                                .Join(_context.Courses,
                                      s => s.Id,
                                      c => c.StudentId,
                                      (s, c) => new
                                      {
                                          StudentName = s.Name,
                                          CourseTitle = c.Title
                                      });
            return View(studentCourses);
        }

        public IActionResult GroupByDepartment()
        {
            var groupedStudents = _context.Students
                .GroupBy(s => s.Department)
                .Select(g => new GroupedStudentViewModel
                {
                    Department = g.Key,
                    Students = g.ToList()
                })
                .ToList();
            return View(groupedStudents);
        }

        // Bazý db iþleri için özel methodlar yazmamýz gerekebilir. Bunlara custom extension method denir.
        // Öðrencilerin yaþ aralýklýðýna göre gruplayan method.
        public IActionResult CustomExtensionMethod()
        {
            var students = _context.Students.ToList();  
            var groupedStudentsByAge = students.GroupByAgeRange(); // Extension method çaðrýsý
            return View(groupedStudentsByAge);
        }

        public IActionResult GetStudentsByDepartment()
        {
            return View();
        }

        // Sql tarafýnda oluþturduðumuz stored procedure'ü çaðýrmak için FromSqlInterpolated kullanýyoruz
        // FromSqlInterpolated, SQL sorgularýný doðrudan Entity Framework Core ile çalýþtýrmak için kullanýlýr ve SQL enjeksiyon saldýrýlarýna karþý koruma saðlar.
        [HttpPost]
        public IActionResult GetStudentsByDepartment(string department)
        {
            var students = _context.Students
                            .FromSqlInterpolated($"EXEC GetStudentsByDepartment {department}")
                            .ToList();
            ViewData["Students"] = students;
            return View(); 
        }

        // RawSql kullanýmý. RawSql, SQL sorgularýný doðrudan Entity Framework Core ile çalýþtýrmak için kullanýlýr ancak SQL enjeksiyon saldýrýlarýna karþý koruma saðlamaz.
        public IActionResult RawSql()
        {
            var students = _context.Students
                            .FromSqlRaw("SELECT * FROM Students WHERE Age > 20")
                            .ToList();
            return View("Index", students);
        }
        // Javascript tarafýndan Ajax isteði ile çaðýrýlacak Transaction örneði
        // Transaction: Bir dizi veritabaný iþleminin tek bir birim olarak ele alýndýðý bir konsepttir.
        // Eðer iþlemlerden biri baþarýsýz olursa, tüm iþlemler geri alýnýr (rollback).
        // Bu, veritabanýnýn tutarlýlýðýný saðlar.
        [HttpPost]
        public IActionResult AddStudentsByTransaction([FromBody] List<Student> students)
        {
            var transaction = _context.Database.BeginTransaction();

            try
            {
                _context.Students.AddRange(students);
                _context.SaveChanges();
                // throw new Exception("Hata"); // test için kayýt edilmediðini görebiliriz.
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                return StatusCode(500, "Öðrenciler Eklenirken bir hata oluþtu");
            }
            return Ok();
        }

        public IActionResult BulkInsert()
        {
            var students = new List<Student>
            {
                new Student { Name = "Ayþe" , Age = 45, Department = "Tekstil"},
                new Student { Name = "Melih" , Age = 25, Department = "Elektronik"},
                new Student { Name = "Efe" , Age = 32, Department = "Bilgisayar"}
            };

            _context.BulkInsert(students);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ExcelBulkInsert(IFormFile file)
        {
            if (file == null || file.Length == 0) return BadRequest("Dosya seçilmedi.");

            var students = new List<Student>();

            // Excel dosyasýný okumak için Stream açýyoruz
            using (var stream = file.OpenReadStream())
            {
                // Kod sayfasýný desteklemek için (Özellikle Türkçe karakterler için önemli)
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    // Ýlk satýrýn baþlýk olduðunu varsayýyoruz, o yüzden okuyup geçiyoruz
                    reader.Read();

                    while (reader.Read()) // Satýr satýr oku
                    {
                        students.Add(new Student
                        {
                            Name = reader.GetValue(0)?.ToString(),       // A Sütunu
                            Age = Convert.ToInt32(reader.GetValue(1)),   // B Sütunu
                            Department = reader.GetValue(2)?.ToString()  // C Sütunu
                        });
                    }
                }
            }

            // Veritabanýna toplu ekleme iþlemi
            if (students.Any())
            {
                _context.BulkInsert(students);  
            }

            List<Student> studentList = _context.Students.ToList();

            return View("Index", studentList);
        }
    }


}
