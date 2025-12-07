using System.Diagnostics;

namespace _16_Middleware.Middlewares
{
    public class CustomAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            // Privacy sayfasına erişim için basit bir yetki kontrolü yapalım
            if (context.Request.Path.StartsWithSegments("/Home/Privacy"))
            {
                // Örnek Yetki Kontrolü: Gerçek uygulamada context.User'ı kullanın.
                // Basitlik için yine query string kontrolü yapalım.
                bool isAdmin = context.User.IsInRole("Admin") ||
                               context.Request.Query.ContainsKey("isAdmin") && 
                               context.Request.Query["isAdmin"] == "true";

                if (!isAdmin)
                {
                    
                    context.Response.Redirect("/Home/AccessDenied");

                    Debug.WriteLine($"Yetki Engellendi: {context.Request.Path} yönlendirildi.");
                    return;
                }
            }

            await _next(context);

            
        }
    }
}
