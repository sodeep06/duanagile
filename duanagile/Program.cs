
using duanagile.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// C?u h�nh DbContext k?t n?i t?i SQL Server
builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

//C? u h�nh d?ch v? x�c th?c v?i Cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/UserLogin/Login"; // ???ng d?n ??n trang ??ng nh?p
options.LogoutPath = "/UserLogin/Logout"; // ???ng d?n ??n trang ??ng xu?t
options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Th?i gian s?ng c?a cookie
options.AccessDeniedPath = "/Home/AccessDenied"; // ???ng d?n khi ng??i d�ng b? t? ch?i quy?n truy c?p
    });

// Th�m c�c d?ch v? kh�c ? ?�y (n?u c�)

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Trang l?i t�y ch?nh
    app.UseHsts(); // B?o m?t HSTS
}

app.UseHttpsRedirection(); // T? ??ng chuy?n h??ng sang HTTPS
app.UseStaticFiles();       // Ph?c v? c�c t?p t?nh (CSS, JS, h�nh ?nh, v.v.)

app.UseRouting();           // ??nh tuy?n c�c y�u c?u HTTP

// C�c Middleware x�c th?c v� ph�n quy?n
app.UseAuthentication();    // X�c th?c ng??i d�ng
app.UseAuthorization();     // Ph�n quy?n ng??i d�ng

// C?u h�nh Endpoints cho c�c Area
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

// C?u h�nh c�c Route m?c ??nh cho c�c controller kh�ng thu?c Area
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

