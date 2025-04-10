
using duanagile.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace duanagile.Controllers
{
    public class FoodItemController : Controller
    {
        private readonly MyDbContext _context;

        public FoodItemController(MyDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Browse()
        {
            var foodItems = await _context.FoodItems.ToListAsync();
            return View(foodItems);
        }

        // GET: /FoodItem/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            var foodItem = await _context.FoodItems.FindAsync(id);
            if (foodItem == null)
            {
                return NotFound();
            }
            return View(foodItem);
        }

        // GET: /FoodItem/Search
        public async Task<IActionResult> Search(string name, decimal? minPrice, decimal? maxPrice, string category)
        {
            var query = _context.FoodItems.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(f => f.Name.Contains(name));
            }
            if (minPrice.HasValue)
            {
                query = query.Where(f => f.Price >= minPrice.Value);
            }
            if (maxPrice.HasValue)
            {
                query = query.Where(f => f.Price <= maxPrice.Value);
            }
            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(f => f.Category == category);
            }

            var results = await query.ToListAsync();
            return View("Browse", results);  // Hiển thị kết quả tìm kiếm trong view "Browse"
        }

    }
}
