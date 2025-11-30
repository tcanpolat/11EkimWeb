namespace _02_Routes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // Rotalarýn tanýmlandýðý kýsým 
            app.MapControllerRoute(
                name: "default", // Varsayýlan rota adý
                pattern: "{controller=Home}/{action=Index}/{id?}"); // Rota deseni

            app.MapControllerRoute(
                name: "about", // Rota adý,
                pattern: "about", // URL deseni
                defaults: new { controller = "Home", action = "About" } // Varsayýlan controller ve action
            );

            app.MapControllerRoute(
                name: "aboutDetail", // Rota adý
                pattern: "about/detail/{id?}", // URL deseni ? id parametresi isteðe baðlý
                defaults: new { controller = "Home", action = "AboutDetail" } // Varsayýlan controller ve action
            );

            app.Run();
        }
    }
}
