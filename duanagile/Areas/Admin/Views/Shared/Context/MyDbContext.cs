//using asm1c4.Models;
//using Microsoft.EntityFrameworkCore;

//namespace asm1c4.Context
//{
//    public class MyDbContext : DbContext
//    {
//        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
//        {

//        }

//        // DbSet cho từng bảng
//        public DbSet<User> Users { get; set; }
//        public DbSet<FoodItem> FoodItems { get; set; }
//        public DbSet<Combo> Combos { get; set; }
//        public DbSet<ComboItem> ComboItems { get; set; }
//        public DbSet<Order> Orders { get; set; }
//        public DbSet<OrderDetail> OrderDetails { get; set; }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);

//            // Thiết lập cho bảng User
//            modelBuilder.Entity<User>()
//                .Property(u => u.Role)
//                .IsRequired();

//            modelBuilder.Entity<User>()
//                .HasIndex(u => u.Email)
//                .IsUnique();

//            // Thiết lập cho bảng FoodItem
//            modelBuilder.Entity<FoodItem>()
//                .Property(f => f.Price)
//                .HasColumnType("decimal(18,2)");

//            // Thiết lập cho bảng Combo
//            modelBuilder.Entity<Combo>()
//                .Property(c => c.Price)
//                .HasColumnType("decimal(18,2)");

//            // Thiết lập cho bảng ComboItem (liên kết Combo và FoodItem)
//            modelBuilder.Entity<ComboItem>()
//                .HasOne(ci => ci.Combo)
//                .WithMany(c => c.ComboItems)
//                .HasForeignKey(ci => ci.ComboID)
//                .OnDelete(DeleteBehavior.Cascade);

//            modelBuilder.Entity<ComboItem>()
//                .HasOne(ci => ci.FoodItem)
//                .WithMany(fi => fi.ComboItems)
//                .HasForeignKey(ci => ci.FoodItemID)
//                .OnDelete(DeleteBehavior.Cascade);

//            // Thiết lập cho bảng Order
//            modelBuilder.Entity<Order>()
//                .Property(o => o.Status)
//                .HasMaxLength(50)
//                .IsRequired();

//            modelBuilder.Entity<Order>()
//                .HasOne(o => o.User)
//                .WithMany(u => u.Orders)
//                .HasForeignKey(o => o.UserID)
//                .OnDelete(DeleteBehavior.Cascade);

//            // Thiết lập cho bảng OrderDetail (liên kết Order và FoodItem)
//            modelBuilder.Entity<OrderDetail>()
//                .Property(od => od.Price)
//                .HasColumnType("decimal(18,2)");

//            modelBuilder.Entity<OrderDetail>()
//                .HasOne(od => od.Order)
//                .WithMany(o => o.OrderDetails)
//                .HasForeignKey(od => od.OrderID)
//                .OnDelete(DeleteBehavior.Cascade);

//            modelBuilder.Entity<OrderDetail>()
//                .HasOne(od => od.FoodItem)
//                .WithMany(fi => fi.OrderDetails)
//                .HasForeignKey(od => od.FoodItemID)
//                .OnDelete(DeleteBehavior.Cascade);



//            base.OnModelCreating(modelBuilder);

//            // Dữ liệu mẫu cho User
//            modelBuilder.Entity<User>().HasData(
//                new User
//                {
//                    UserID = 1,
//                    Name = "Admin",
//                    Email = "dhieu6503@gmail.com",
//                    Phone = "0123456789",
//                    PasswordHash = "12345678",
//                    Address = "123 Admin Lane",
//                    DateOfBirth = new DateTime(2005, 1, 1),
//                    Role = "Admin"
//                },
//                new User
//                {
//                    UserID = 2,
//                    Name = "sati",
//                    Email = "sati@gmail.com",
//                    Phone = "0987654321",
//                    PasswordHash = "123456",
//                    Address = "456 Customer Street",
//                    DateOfBirth = new DateTime(1995, 5, 15),
//                    Role = "Customer"
//                },
//                new User
//                {
//                    UserID = 3,
//                    Name = "Guest User",
//                    Email = "guest@example.com",
//                    Phone = "1112223333",
//                    PasswordHash = "hashedpassword789",
//                    Address = "789 Guest Avenue",
//                    DateOfBirth = null,
//                    Role = "Guest"
//                }
//            );
//            modelBuilder.Entity<Combo>().HasData(
//       new Combo
//       {
//           ComboID = 1,
//           Name = "Combo Bữa Sáng",
//           Description = "Bao gồm bánh mì, trứng chiên, sữa tươi",
//           Price = 99_000,
//           ImageURL = "/images/buasang1.jpg"
//       },
//       new Combo
//       {
//           ComboID = 2,
//           Name = "Combo Trưa Nhanh",
//           Description = "Cơm gà, cánh gà, Kim chi.",
//           Price = 129_000,
//           ImageURL = "/images/buatrua.jpg"
//       },
//       new Combo
//       {
//           ComboID = 3,
//           Name = "Combo Đặc Biệt",
//           Description = "cánh gà, khoai tây chiên, pepsi, bánh rán.",
//           Price = 199_000,
//           ImageURL = "/images/dacbiet.jpg"
//       }

//       );
//            modelBuilder.Entity<FoodItem>().HasData(
//                new FoodItem
//                {
//                    FoodItemID = 1,
//                    Name = "Bánh mì",
//                    Price = 10_000,
//                    Description = "Bánh mì truyền thống.",
//                    ImageURL = "/images/bmtt.jpg",
//                    Category = "Đồ ăn sáng",
//                    Ingredients = "Bánh mì, thịt heo, dưa leo, rau thơm, pate"
//                },
//                new FoodItem
//                {
//                    FoodItemID = 2,
//                    Name = "Trứng chiên",
//                    Price = 5_000,
//                    Description = "Trứng chiên giòn.",
//                    ImageURL = "/images/trungchien.jpg",
//                    Category = "Đồ ăn sáng",
//                    Ingredients = "Trứng, dầu ăn, gia vị"
//                },
//                new FoodItem
//                {
//                    FoodItemID = 3,
//                    Name = "Cơm gà xối mỡ",
//                    Price = 50_000,
//                    Description = "Cơm gà thơm ngon.",
//                    ImageURL = "/images/congphuong.jpg",
//                    Category = "Đồ ăn trưa",
//                    Ingredients = "Cơm, gà xối mỡ, rau sống, gia vị"
//                },
//                new FoodItem
//                {
//                    FoodItemID = 4,
//                    Name = "Canh rau củ",
//                    Price = 15_000,
//                    Description = "Canh rau củ bổ dưỡng.",
//                    ImageURL = "/images/cảnhaucu.jpg",
//                    Category = "Đồ ăn trưa",
//                    Ingredients = "Cà rốt, khoai tây, rau mùi, nước dùng"
//                },
//                new FoodItem
//                {
//                    FoodItemID = 5,
//                    Name = "Pizza hải sản",
//                    Price = 120_000,
//                    Description = "Pizza tươi ngon.",
//                    ImageURL = "/images/pizaaahs.jpg",
//                    Category = "Đồ ăn tối",
//                    Ingredients = "Bột mì, phô mai, tôm, mực, sốt cà chua"
//                },
//                new FoodItem
//                {
//                    FoodItemID = 6,
//                    Name = "Khoai tây chiên",
//                    Price = 20_000,
//                    Description = "Khoai tây giòn rụm.",
//                    ImageURL = "/images/khoai.jpg",
//                    Category = "Đồ ăn vặt",
//                    Ingredients = "Khoai tây, dầu ăn, gia vị"
//                }
//            );

//            modelBuilder.Entity<ComboItem>().HasData(
//    new ComboItem
//    {
//        ComboItemID = 1,
//        ComboID = 1, // Liên kết với Combo Bữa Sáng
//        FoodItemID = 1 // Ví dụ: Bánh mì
//    },
//    new ComboItem
//    {
//        ComboItemID = 2,
//        ComboID = 1, // Liên kết với Combo Bữa Sáng
//        FoodItemID = 2 // Ví dụ: Trứng chiên
//    },
//    new ComboItem
//    {
//        ComboItemID = 3,
//        ComboID = 2, // Liên kết với Combo Trưa Nhanh
//        FoodItemID = 3 // Ví dụ: Cơm gà xối mỡ
//    },
//    new ComboItem
//    {
//        ComboItemID = 4,
//        ComboID = 2, // Liên kết với Combo Trưa Nhanh
//        FoodItemID = 4 // Ví dụ: Canh rau củ
//    },
//    new ComboItem
//    {
//        ComboItemID = 5,
//        ComboID = 3, // Liên kết với Combo Đặc Biệt
//        FoodItemID = 5 // Ví dụ: Pizza hải sản
//    },
//    new ComboItem
//    {
//        ComboItemID = 6,
//        ComboID = 3, // Liên kết với Combo Đặc Biệt
//        FoodItemID = 6 // Ví dụ: Khoai tây chiên
//    }
//);
//        }

//        }
//    }

using duanagile.Models;
using Microsoft.EntityFrameworkCore;

namespace asm1c4.Context
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }

        // DbSet cho từng bảng
        public DbSet<User> Users { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }
        public DbSet<Combo> Combos { get; set; }
        public DbSet<ComboItem> ComboItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Thiết lập cho bảng User
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Thiết lập cho bảng FoodItem
            modelBuilder.Entity<FoodItem>()
                .Property(f => f.Price)
                .HasColumnType("decimal(18,2)");

            // Thiết lập cho bảng Combo
            modelBuilder.Entity<Combo>()
                .Property(c => c.Price)
                .HasColumnType("decimal(18,2)");

            // Thiết lập cho bảng ComboItem (liên kết Combo và FoodItem)
            modelBuilder.Entity<ComboItem>()
                .HasOne(ci => ci.Combo)
                .WithMany(c => c.ComboItems)
                .HasForeignKey(ci => ci.ComboID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ComboItem>()
                .HasOne(ci => ci.FoodItem)
                .WithMany(fi => fi.ComboItems)
                .HasForeignKey(ci => ci.FoodItemID)
                .OnDelete(DeleteBehavior.Cascade);

            // Thiết lập cho bảng Order
            modelBuilder.Entity<Order>()
                .Property(o => o.Status)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            // Thiết lập cho bảng OrderDetail (liên kết Order và FoodItem)
            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.FoodItem)
                .WithMany(fi => fi.OrderDetails)
                .HasForeignKey(od => od.FoodItemID)
                .OnDelete(DeleteBehavior.Cascade);



            base.OnModelCreating(modelBuilder);

            // Dữ liệu mẫu cho User
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserID = 1,
                    Name = "Admin",
                    Email = "dhieu6503@gmail.com",
                    Phone = "0123456789",
                    PasswordHash = "12345678",
                    Address = "123 Admin Lane",
                    DateOfBirth = new DateTime(2005, 1, 1),
                    Role = "Admin"
                },
                new User
                {
                    UserID = 2,
                    Name = "sati",
                    Email = "sati@gmail.com",
                    Phone = "0987654321",
                    PasswordHash = "123456",
                    Address = "456 Customer Street",
                    DateOfBirth = new DateTime(1995, 5, 15),
                    Role = "Customer"
                },
                new User
                {
                    UserID = 3,
                    Name = "Guest User",
                    Email = "guest@example.com",
                    Phone = "1112223333",
                    PasswordHash = "hashedpassword789",
                    Address = "789 Guest Avenue",
                    DateOfBirth = null,
                    Role = "Guest"
                }
            );
            modelBuilder.Entity<Combo>().HasData(
       new Combo
       {
           ComboID = 1,
           Name = "Combo Bữa Sáng",
           Description = "Bao gồm bánh mì, trứng chiên, sữa tươi",
           Price = 99_000,
           ImageURL = "/images/buasang1.jpg"
       },
       new Combo
       {
           ComboID = 2,
           Name = "Combo Trưa Nhanh",
           Description = "Cơm gà, cánh gà, Kim chi.",
           Price = 129_000,
           ImageURL = "/images/buatrua.jpg"
       },
       new Combo
       {
           ComboID = 3,
           Name = "Combo Đặc Biệt",
           Description = "cánh gà, khoai tây chiên, pepsi, bánh rán.",
           Price = 199_000,
           ImageURL = "/images/dacbiet.jpg"
       }

       );
            modelBuilder.Entity<FoodItem>().HasData(
                new FoodItem
                {
                    FoodItemID = 1,
                    Name = "Bánh mì",
                    Price = 10_000,
                    Description = "Bánh mì truyền thống.",
                    ImageURL = "/images/bmtt.jpg",
                    Category = "Đồ ăn sáng",
                    Ingredients = "Bánh mì, thịt heo, dưa leo, rau thơm, pate"
                },
                new FoodItem
                {
                    FoodItemID = 2,
                    Name = "Trứng chiên",
                    Price = 5_000,
                    Description = "Trứng chiên giòn.",
                    ImageURL = "/images/trungchien.jpg",
                    Category = "Đồ ăn sáng",
                    Ingredients = "Trứng, dầu ăn, gia vị"
                },
                new FoodItem
                {
                    FoodItemID = 3,
                    Name = "Cơm gà xối mỡ",
                    Price = 50_000,
                    Description = "Cơm gà thơm ngon.",
                    ImageURL = "/images/congphuong.jpg",
                    Category = "Đồ ăn trưa",
                    Ingredients = "Cơm, gà xối mỡ, rau sống, gia vị"
                },
                new FoodItem
                {
                    FoodItemID = 4,
                    Name = "Canh rau củ",
                    Price = 15_000,
                    Description = "Canh rau củ bổ dưỡng.",
                    ImageURL = "/images/cảnhaucu.jpg",
                    Category = "Đồ ăn trưa",
                    Ingredients = "Cà rốt, khoai tây, rau mùi, nước dùng"
                },
                new FoodItem
                {
                    FoodItemID = 5,
                    Name = "Pizza hải sản",
                    Price = 120_000,
                    Description = "Pizza tươi ngon.",
                    ImageURL = "/images/pizaaahs.jpg",
                    Category = "Đồ ăn tối",
                    Ingredients = "Bột mì, phô mai, tôm, mực, sốt cà chua"
                },
                new FoodItem
                {
                    FoodItemID = 6,
                    Name = "Khoai tây chiên",
                    Price = 20_000,
                    Description = "Khoai tây giòn rụm.",
                    ImageURL = "/images/khoai.jpg",
                    Category = "Đồ ăn vặt",
                    Ingredients = "Khoai tây, dầu ăn, gia vị"
                }
            );

            modelBuilder.Entity<ComboItem>().HasData(
    new ComboItem
    {
        ComboItemID = 1,
        ComboID = 1, // Liên kết với Combo Bữa Sáng
        FoodItemID = 1 // Ví dụ: Bánh mì
    },
    new ComboItem
    {
        ComboItemID = 2,
        ComboID = 1, // Liên kết với Combo Bữa Sáng
        FoodItemID = 2 // Ví dụ: Trứng chiên
    },
    new ComboItem
    {
        ComboItemID = 3,
        ComboID = 2, // Liên kết với Combo Trưa Nhanh
        FoodItemID = 3 // Ví dụ: Cơm gà xối mỡ
    },
    new ComboItem
    {
        ComboItemID = 4,
        ComboID = 2, // Liên kết với Combo Trưa Nhanh
        FoodItemID = 4 // Ví dụ: Canh rau củ
    },
    new ComboItem
    {
        ComboItemID = 5,
        ComboID = 3, // Liên kết với Combo Đặc Biệt
        FoodItemID = 5 // Ví dụ: Pizza hải sản
    },
    new ComboItem
    {
        ComboItemID = 6,
        ComboID = 3, // Liên kết với Combo Đặc Biệt
        FoodItemID = 6 // Ví dụ: Khoai tây chiên
    }
);
        }

    }
}

