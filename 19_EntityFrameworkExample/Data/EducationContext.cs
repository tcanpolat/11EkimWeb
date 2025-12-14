using _19_EntityFrameworkExample.Models;
using Microsoft.EntityFrameworkCore;

namespace _19_EntityFrameworkExample.Data
{
    public class EducationContext : DbContext
    {
        public EducationContext(DbContextOptions<EducationContext> options) : base(options)
        {
        }

        // Veritabanı tablolarını temsil eden DbSet'ler
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        // Onmodel creating metodu ile ilişki yapılandırmaları yapılabilir
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Öğrenci ve Kurs arasındaki ilişkiyi yapılandırma
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Student) // Her kursun bir öğrencisi vardır
                .WithMany(s => s.Courses) // Her öğrencinin birden fazla kursu olabilir
                .HasForeignKey(c => c.StudentId); // Yabancı anahtar StudentId
        }



    }
}
