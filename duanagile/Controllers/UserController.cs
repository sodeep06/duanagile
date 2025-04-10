using duanagile.Context;
using duanagile.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace duanagile.Controllers
{
    public class UserController : Controller
    {
        private readonly MyDbContext _context;
        public UserController(MyDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Register()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {

            user.Role = "Customer";
            //var passwordHasher = new PasswordHasher<User>();
            //user.PasswordHash = passwordHasher.HashPassword(user, user.PasswordHash);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}

