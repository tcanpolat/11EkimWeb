using _16_Middleware.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
/* 
    Middleware Nedir?
    Middleware, bir web uygulamasýnda gelen HTTP isteklerini iþleyen ve yanýtlarý oluþturan
    yazýlým bileþenleridir. ASP.NET Core'da middleware'ler, isteklerin iþlenme sürecinde
    belirli görevleri yerine getirmek için ardýþýk olarak zincirlenmiþ bileþenlerdir.
    
    Middleware'ler, uygulamanýn istek iþleme boru hattýný (pipeline) oluþturur ve her
    middleware, isteði alýr, üzerinde iþlem yapar ve isteði bir sonraki middleware'e
    iletebilir veya yanýtý doðrudan döndürebilir.
    Middleware'lerin bazý yaygýn kullanýmlarý þunlardýr:
    - Kimlik doðrulama ve yetkilendirme
    - Hata iþleme
    - Ýstek yönlendirme
    - Statik dosya sunumu
    - Günlük kaydý (logging)
    - CORS (Cross-Origin Resource Sharing) yönetimi
    ASP.NET Core'da middleware'ler, `Use` metotlarý aracýlýðýyla yapýlandýrýlýr ve
    `Program.cs` dosyasýnda tanýmlanabilir.
 
 
 
 */
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseMiddleware<RequestTimingMiddleware>();
app.UseMiddleware<CustomAuthorizationMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
