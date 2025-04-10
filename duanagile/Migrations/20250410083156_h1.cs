using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace duanagile.Migrations
{
    /// <inheritdoc />
    public partial class h1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    CartID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.CartID);
                });

            migrationBuilder.CreateTable(
                name: "Combos",
                columns: table => new
                {
                    ComboID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Combos", x => x.ComboID);
                });

            migrationBuilder.CreateTable(
                name: "FoodItems",
                columns: table => new
                {
                    FoodItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ingredients = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodItems", x => x.FoodItemID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    CartItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartID = table.Column<int>(type: "int", nullable: false),
                    FoodItemID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.CartItemID);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartID",
                        column: x => x.CartID,
                        principalTable: "Carts",
                        principalColumn: "CartID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_FoodItems_FoodItemID",
                        column: x => x.FoodItemID,
                        principalTable: "FoodItems",
                        principalColumn: "FoodItemID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComboItems",
                columns: table => new
                {
                    ComboItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComboID = table.Column<int>(type: "int", nullable: false),
                    FoodItemID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComboItems", x => x.ComboItemID);
                    table.ForeignKey(
                        name: "FK_ComboItems_Combos_ComboID",
                        column: x => x.ComboID,
                        principalTable: "Combos",
                        principalColumn: "ComboID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComboItems_FoodItems_FoodItemID",
                        column: x => x.FoodItemID,
                        principalTable: "FoodItems",
                        principalColumn: "FoodItemID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    FoodItemID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.OrderDetailID);
                    table.ForeignKey(
                        name: "FK_OrderDetails_FoodItems_FoodItemID",
                        column: x => x.FoodItemID,
                        principalTable: "FoodItems",
                        principalColumn: "FoodItemID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Combos",
                columns: new[] { "ComboID", "Description", "ImageURL", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Bao gồm bánh mì, trứng chiên, sữa tươi", "/images/buasang1.jpg", "Combo Bữa Sáng", 99000m },
                    { 2, "Cơm gà, cánh gà, Kim chi.", "/images/buatrua.jpg", "Combo Trưa Nhanh", 129000m },
                    { 3, "cánh gà, khoai tây chiên, pepsi, bánh rán.", "/images/dacbiet.jpg", "Combo Đặc Biệt", 199000m }
                });

            migrationBuilder.InsertData(
                table: "FoodItems",
                columns: new[] { "FoodItemID", "Category", "Description", "ImageURL", "Ingredients", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Dạng sữa", "Kem chống nắng dạng sữa, không thấm nước, phù hợp da dầu.", "/images/sp1.jpg", "Zinc Oxide, Octocrylene, Hyaluronic Acid", "Anessa Perfect UV Sunscreen SPF50+", 350000m },
                    { 2, "Dạng kem", "Kem chống nắng cho da nhạy cảm, thẩm thấu nhanh, không gây bết dính.", "/images/sp2.jpg", "Mexoryl SX/XL, Glycerin, Vitamin E", "La Roche-Posay Anthelios XL SPF50+", 420000m },
                    { 3, "Dạng gel", "Kết cấu nhẹ như nước, thấm nhanh, không gây bí da.", "/images/sp3.jpg", "Alcohol, Hyaluronic Acid, Royal Jelly Extract", "Biore UV Aqua Rich Watery Essence SPF50+", 230000m },
                    { 4, "Nâng tone", "Kem chống nắng nâng tone, phù hợp dùng làm lớp lót makeup.", "/images/sp4.jpg", "Vitamin C, Lavender Extract, Hyaluronic Acid", "Skin Aqua Tone Up UV Essence SPF50+", 290000m },
                    { 5, "Dạng kem", "Chống nắng kiêm dưỡng da, phù hợp da hỗn hợp và da dầu.", "/images/sp5.jpg", "Mexoryl XL, Vitamin E, Thermal Water", "Vichy Capital Soleil SPF50+", 480000m },
                    { 6, "Dạng kem", "Kem chống nắng nhẹ dịu chiết xuất trà xanh, phù hợp cho da thường và da khô.", "/images/sp6.jpg", "Green Tea Extract, Titanium Dioxide, Centella Asiatica", "Innisfree Daily UV Defense SPF36", 190000m }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Address", "DateOfBirth", "Email", "Name", "PasswordHash", "Phone", "Role" },
                values: new object[,]
                {
                    { 1, "123 Admin Lane", new DateTime(2005, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "dhieu6503@gmail.com", "Admin", "12345678", "0123456789", "Admin" },
                    { 2, "456 Customer Street", new DateTime(1995, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "sati@example.com", "sati", "hashedpassword456", "0987654321", "Customer" },
                    { 3, "789 Guest Avenue", null, "guest@example.com", "Guest User", "hashedpassword789", "1112223333", "Guest" }
                });

            migrationBuilder.InsertData(
                table: "ComboItems",
                columns: new[] { "ComboItemID", "ComboID", "FoodItemID" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 2, 3 },
                    { 4, 2, 4 },
                    { 5, 3, 5 },
                    { 6, 3, 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartID",
                table: "CartItems",
                column: "CartID");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_FoodItemID",
                table: "CartItems",
                column: "FoodItemID");

            migrationBuilder.CreateIndex(
                name: "IX_ComboItems_ComboID",
                table: "ComboItems",
                column: "ComboID");

            migrationBuilder.CreateIndex(
                name: "IX_ComboItems_FoodItemID",
                table: "ComboItems",
                column: "FoodItemID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_FoodItemID",
                table: "OrderDetails",
                column: "FoodItemID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderID",
                table: "OrderDetails",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserID",
                table: "Orders",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "ComboItems");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Combos");

            migrationBuilder.DropTable(
                name: "FoodItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
