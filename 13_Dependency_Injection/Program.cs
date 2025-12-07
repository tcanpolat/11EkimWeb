using _13_Dependency_Injection.Services.Abstract;
using _13_Dependency_Injection.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
/* 
    LifeCycle (Yaþam Döngüsü) Yönetimi:
    IMyService ve MyService Container'a ekleniyor.
    NET Core Depencency Injection servislerinin ömrü (lifecycle) belirlemek için
    üç farklý yöntem sunar:
    AddSingelton,AddScoped,AddTransient => Her birinin avantaj ve dezavantajlarý vardýr.

    1. AddTransient:
       - Her istek için yeni bir örnek oluþturur.
       - Kýsa ömürlü servisler için uygundur.
       - Hafýza kullanýmý daha yüksektir çünkü her istek yeni bir nesne oluþturur.
    2. AddScoped:
        - Her istek (HTTP request) için tek bir örnek oluþturur.
        - Web uygulamalarýnda yaygýn olarak kullanýlýr.
        - Ayný istek içinde paylaþýlan veriler için uygundur.
    3. AddSingleton:
        - Uygulama ömrü boyunca tek bir örnek oluþturur.
        - Hafýza kullanýmý düþüktür çünkü tek bir nesne paylaþýlýr.
        - Paylaþýlan durum (state) yönetimi için dikkatli kullanýlmalýdýr, çünkü tüm istekler ayný nesneyi kullanýr.
 
 
 
 */
builder.Services.AddScoped<IMyService, MyService>();


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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
