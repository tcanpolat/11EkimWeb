using _19_EntityFrameworkExample.Data;
using _19_EntityFrameworkExample.Models;
using _19_EntityFrameworkExample.ViewModels;
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
    }

   
}
