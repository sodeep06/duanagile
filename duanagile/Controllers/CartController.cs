using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using duanagile.Context;
using duanagile.Models;

namespace asm1c4.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CartController : Controller
    {
        private readonly MyDbContext _context;

        public CartController(MyDbContext context)
        {
            _context = context;
        }

        // Hiển thị giỏ hàng
        public async Task<IActionResult> Index()
        {
            var userId = 1; // Lấy ID người dùng từ hệ thống xác thực
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.FoodItem)
                .FirstOrDefaultAsync(c => c.UserID == userId);

            if (cart == null || !cart.CartItems.Any())
            {
                ViewBag.Message = "Giỏ hàng của bạn đang trống.";
                return View(new List<CartItem>());
            }

            return View(cart.CartItems);
        }
        //[HttpPost]
        //public async Task<IActionResult> UpdateCartItem(int cartItemId, int newQuantity)
        //{
        //    if (newQuantity <= 0)
        //    {
        //        return BadRequest("Số lượng không hợp lệ.");
        //    }

        //    // Tìm mục trong giỏ hàng
        //    var cartItem = await _context.CartItems
        //        .Include(ci => ci.Cart)          // Bao gồm thông tin giỏ hàng
        //        .Include(ci => ci.FoodItem)      // Bao gồm thông tin món ăn
        //        .FirstOrDefaultAsync(ci => ci.CartItemID == cartItemId);

        //    if (cartItem == null)
        //    {
        //        return NotFound("Không tìm thấy mặt hàng trong giỏ.");
        //    }

        //    // Tạm thời dùng userId cố định để kiểm tra quyền
        //    var userId = 1; // Thay bằng giá trị người dùng từ hệ thống xác thực sau này
        //    // Kiểm tra quyền sở hữu (giỏ hàng phải thuộc về người dùng hiện tại)
        //    if (cartItem.Cart.UserID != userId)
        //    {
        //        return Unauthorized("Bạn không có quyền chỉnh sửa giỏ hàng này.");
        //    }

        //    // Cập nhật số lượng và giá
        //    cartItem.Quantity = newQuantity;

        //    // Đảm bảo rằng FoodItem không bị null trước khi truy cập Price
        //    if (cartItem.FoodItem != null)
        //    {
        //        cartItem.Price = cartItem.Quantity * cartItem.FoodItem.Price;
        //    }
        //    else
        //    {
        //        return BadRequest("Không thể cập nhật vì thiếu thông tin món ăn.");
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        public async Task<IActionResult> UpdateCartItem([FromForm] int cartItemId, [FromForm] int newQuantity)
        {
            // Debug giá trị để kiểm tra
            if (newQuantity <= 0)
            {
                return BadRequest($"Số lượng phải lớn hơn 0. Giá trị nhận được: {newQuantity}");
            }

            var cartItem = await _context.CartItems
                .Include(ci => ci.Cart)
                .Include(ci => ci.FoodItem)
                .FirstOrDefaultAsync(ci => ci.CartItemID == cartItemId);

            if (cartItem == null)
            {
                return NotFound("Không tìm thấy mặt hàng trong giỏ.");
            }

            var userId = 1; // Thay bằng hệ thống xác thực thực tế
            if (cartItem.Cart.UserID != userId)
            {
                return Unauthorized("Bạn không có quyền chỉnh sửa giỏ hàng này.");
            }

            cartItem.Quantity = newQuantity;
            cartItem.Price = cartItem.Quantity * cartItem.FoodItem.Price;

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }





        // Thêm món ăn vào giỏ hàng
        [HttpPost]
        public async Task<IActionResult> AddToCart(int foodItemId, int quantity = 1)
        {
            if (quantity <= 0)
            {
                return BadRequest("Số lượng không hợp lệ.");
            }

            var userId = 1; // Lấy ID người dùng từ hệ thống xác thực
            var foodItem = await _context.FoodItems.FindAsync(foodItemId);

            if (foodItem == null)
            {
                return NotFound("Không tìm thấy món ăn.");
            }

            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserID == userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserID = userId
                };
                _context.Carts.Add(cart);
            }

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.FoodItemID == foodItemId);
            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
                cartItem.Price += foodItem.Price * quantity;
            }
            else
            {
                cart.CartItems.Add(new CartItem
                {
                    FoodItemID = foodItemId,
                    Quantity = quantity,
                    Price = foodItem.Price * quantity
                });
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem == null)
            {
                return NotFound("Không tìm thấy món ăn trong giỏ hàng.");
            }

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // Thanh toán
        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            var userId = 1; // Lấy ID người dùng từ hệ thống xác thực
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserID == userId);

            if (cart == null || !cart.CartItems.Any())
            {
                return BadRequest("Giỏ hàng của bạn đang trống.");
            }

            var order = new Order
            {
                UserID = userId,
                OrderDate = DateTime.Now,
                Status = "Pending",
                OrderDetails = cart.CartItems.Select(ci => new OrderDetail
                {
                    FoodItemID = ci.FoodItemID,
                    Quantity = ci.Quantity,
                    Price = ci.Price
                }).ToList()
            };

            _context.Orders.Add(order);
            _context.CartItems.RemoveRange(cart.CartItems);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Orders");
        }
    }
}
