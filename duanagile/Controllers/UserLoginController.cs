


using duanagile.Context;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Linq;
using duanagile.Context;

namespace duanagile.Controllers
{
    public class UserLoginController : Controller
    {
        private readonly MyDbContext _context;

        public UserLoginController(MyDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            // Kiểm tra thông tin người dùng từ database
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.PasswordHash == password);

            if (user == null)
            {
                ViewBag.ErrorMessage = "Email hoặc mật khẩu không đúng.";
                return View();
            }

            // Tạo danh sách Claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            // Đăng nhập người dùng
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            // Điều hướng theo vai trò (nếu có)
            if (user.Role == "Admin")
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            else if (user.Role == "Customer")
            {
                return RedirectToAction("Index", "Customer");
            }

            // Mặc định nếu không có role nào khớp
            return RedirectToAction("Index", "Home");
        }
        // Đăng xuất người dùng
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
