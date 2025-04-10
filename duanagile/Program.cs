
using duanagile.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// C?u hình DbContext k?t n?i t?i SQL Server
builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

//C? u hình d?ch v? xác th?c v?i Cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/UserLogin/Login"; // ???ng d?n ??n trang ??ng nh?p
options.LogoutPath = "/UserLogin/Logout"; // ???ng d?n ??n trang ??ng xu?t
options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Th?i gian s?ng c?a cookie
options.AccessDeniedPath = "/Home/AccessDenied"; // ???ng d?n khi ng??i dùng b? t? ch?i quy?n truy c?p
    });

// Thêm các d?ch v? khác ? ?ây (n?u có)

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Trang l?i tùy ch?nh
    app.UseHsts(); // B?o m?t HSTS
}

app.UseHttpsRedirection(); // T? ??ng chuy?n h??ng sang HTTPS
app.UseStaticFiles();       // Ph?c v? các t?p t?nh (CSS, JS, hình ?nh, v.v.)

app.UseRouting();           // ??nh tuy?n các yêu c?u HTTP

// Các Middleware xác th?c và phân quy?n
app.UseAuthentication();    // Xác th?c ng??i dùng
app.UseAuthorization();     // Phân quy?n ng??i dùng

// C?u hình Endpoints cho các Area
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

// C?u hình các Route m?c ??nh cho các controller không thu?c Area
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

