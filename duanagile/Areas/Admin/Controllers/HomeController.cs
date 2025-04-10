using duanagile.Context;
using duanagile.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace duanagile.Areas.Admin.Controllers
{
    [Area ("Admin")]
    public class HomeController : Controller
    {
        private readonly MyDbContext _context;

        public HomeController(MyDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> ManageFoodItems()
        {
            var foodItems = await _context.FoodItems.ToListAsync();
            return View(foodItems);
        }

        // Trang quản lý đơn hàng
        public async Task<IActionResult> ManageOrders()
        {
            var orders = await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.FoodItem)
                .ToListAsync();

            return View(orders);
        }

        // Trang quản lý giỏ hàng
        public async Task<IActionResult> ManageCarts()
        {
            var carts = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.FoodItem)
                .ToListAsync();

            return View(carts);
        }
        public IActionResult Index()
        {
            return View();
        }

        // Thêm món ăn mới
        [HttpGet]
        public IActionResult AddFoodItem()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFoodItem(FoodItem foodItem)
        {
            if (ModelState.IsValid)
            {
                _context.FoodItems.Add(foodItem);
                await _context.SaveChangesAsync();
                return RedirectToAction("ManageFoodItems");
            }

            return View(foodItem);
        }

        // Chỉnh sửa món ăn
        [HttpGet]
        public async Task<IActionResult> EditFoodItem(int id)
        {
            var foodItem = await _context.FoodItems.FindAsync(id);
            if (foodItem == null)
            {
                return NotFound();
            }

            return View(foodItem);
        }

        [HttpPost]
        public async Task<IActionResult> EditFoodItem(FoodItem foodItem)
        {
            if (ModelState.IsValid)
            {
                _context.Update(foodItem);
                await _context.SaveChangesAsync();
                return RedirectToAction("ManageFoodItems");
            }

            return View(foodItem);
        }

        // Xóa món ăn
        [HttpPost]
        public async Task<IActionResult> DeleteFoodItem(int id)
        {
            var foodItem = await _context.FoodItems.FindAsync(id);
            if (foodItem != null)
            {
                _context.FoodItems.Remove(foodItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("ManageFoodItems");
        }

        // Xác nhận đơn hàng (Hoàn tất đơn hàng)
        [HttpPost]
        public async Task<IActionResult> CompleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                order.Status = "Completed";
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("ManageOrders");
        }
    }

}

