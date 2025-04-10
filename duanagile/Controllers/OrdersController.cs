
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Authorization;
using duanagile.Context;
using duanagile.Models;

namespace duanagile.Controllers
{
    [Authorize(Roles = "Customer")]
    public class OrdersController : Controller
    {
        private readonly MyDbContext _context;

        public OrdersController(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = 1; // Thay bằng User.Identity.UserId khi tích hợp Identity

            var orders = await _context.Orders
                .Where(o => o.UserID == userId)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.FoodItem)
                .ToListAsync();

            return View(orders); // Trả về danh sách đơn hàng tới view Index
        }


        // Hiển thị chi tiết đơn hàng
        public async Task<IActionResult> Details(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.FoodItem)
                .FirstOrDefaultAsync(o => o.OrderID == id);

            if (order == null)
            {
                return NotFound("Không tìm thấy đơn hàng.");
            }

            return View(order);
        }


        [HttpPost]
        public async Task<IActionResult> AddFoodItem(int foodItemId, int quantity = 1)
        {
            if (quantity <= 0)
            {
                return BadRequest("Số lượng không hợp lệ.");
            }

            var userId = 1; // Thay bằng User.Identity.UserId khi tích hợp Identity

            var foodItem = await _context.FoodItems.FindAsync(foodItemId);
            if (foodItem == null)
            {
                return NotFound("Không tìm thấy món ăn.");
            }

            // Tạo hoặc lấy đơn hàng đang chờ xử lý của người dùng
            var order = await _context.Orders
                .Include(o => o.OrderDetails) // Đảm bảo OrderDetails được tải
                .FirstOrDefaultAsync(o => o.UserID == userId && o.Status == "Pending");

            if (order == null)
            {
                order = new Order
                {
                    UserID = userId,
                    OrderDate = DateTime.Now,
                    Status = "Pending",
                    OrderDetails = new List<OrderDetail>() // Khởi tạo danh sách trống
                };

                _context.Orders.Add(order);
            }
            else if (order.OrderDetails == null)
            {
                // Nếu OrderDetails vẫn null, khởi tạo danh sách trống
                order.OrderDetails = new List<OrderDetail>();
            }

            // Thêm món ăn vào đơn hàng
            var orderDetail = order.OrderDetails.FirstOrDefault(od => od.FoodItemID == foodItemId);
            if (orderDetail != null)
            {
                // Cập nhật số lượng nếu món ăn đã tồn tại trong đơn hàng
                orderDetail.Quantity += quantity;
                orderDetail.Price += foodItem.Price * quantity;
            }
            else
            {
                // Thêm món ăn mới
                order.OrderDetails.Add(new OrderDetail
                {
                    FoodItemID = foodItemId,
                    Quantity = quantity,
                    Price = foodItem.Price * quantity
                });
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }



        // Đặt combo
        [HttpPost]
        public async Task<IActionResult> AddCombo(int comboId)
        {
            var userId = 1; // Thay bằng User.Identity.UserId khi tích hợp Identity

            var combo = await _context.Combos
                .Include(c => c.ComboItems)
                .ThenInclude(ci => ci.FoodItem)
                .FirstOrDefaultAsync(c => c.ComboID == comboId);

            if (combo == null)
            {
                return NotFound("Không tìm thấy combo.");
            }

            // Tạo hoặc lấy đơn hàng đang chờ xử lý của người dùng
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.UserID == userId && o.Status == "Pending");

            if (order == null)
            {
                order = new Order
                {
                    UserID = userId,
                    OrderDate = DateTime.Now,
                    Status = "Pending",
                    OrderDetails = new List<OrderDetail>()
                };

                _context.Orders.Add(order);
            }

            // Thêm từng món ăn trong combo vào đơn hàng
            foreach (var comboItem in combo.ComboItems)
            {
                var orderDetail = order.OrderDetails.FirstOrDefault(od => od.FoodItemID == comboItem.FoodItemID);
                if (orderDetail != null)
                {
                    orderDetail.Quantity += 1;
                    orderDetail.Price += comboItem.FoodItem.Price;
                }
                else
                {
                    order.OrderDetails.Add(new OrderDetail
                    {
                        FoodItemID = comboItem.FoodItemID,
                        Quantity = 1,
                        Price = comboItem.FoodItem.Price
                    });
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // Xác nhận đơn hàng
        [HttpPost]
        public async Task<IActionResult> ConfirmOrder(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderID == id);

            if (order == null)
            {
                return NotFound("Không tìm thấy đơn hàng.");
            }

            order.Status = "Completed";
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> OrderFood(int foodItemId)
        {
            var foodItem = await _context.FoodItems.FindAsync(foodItemId);

            if (foodItem == null)
            {
                return NotFound("Không tìm thấy món ăn.");
            }

            return View(foodItem);
        }

        [HttpPost]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            var userId = 1; // Thay bằng User.Identity.UserId khi tích hợp Identity

            // Tìm đơn hàng của người dùng
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.OrderID == orderId && o.UserID == userId);

            if (order == null)
            {
                return NotFound("Không tìm thấy đơn hàng hoặc bạn không có quyền hủy đơn hàng này.");
            }

            // Chỉ cho phép hủy đơn hàng nếu nó đang ở trạng thái "Pending"
            if (order.Status != "Pending")
            {
                return BadRequest("Chỉ có thể hủy đơn hàng đang chờ xử lý.");
            }

            // Cập nhật trạng thái đơn hàng thành "Cancelled"
            order.Status = "Cancelled";

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


    }
}
