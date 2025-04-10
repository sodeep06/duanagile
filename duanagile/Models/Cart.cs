namespace duanagile.Models
{
    public class Cart
    
        {
            public int CartID { get; set; }
            public int UserID { get; set; } // Người dùng sở hữu giỏ hàng
            public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        }

        public class CartItem
        {
            public int CartItemID { get; set; }
            public int CartID { get; set; }
            public Cart Cart { get; set; }
            public int FoodItemID { get; set; }
            public FoodItem FoodItem { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
        }

    }

