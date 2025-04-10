using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace duanagile.Models
{
    public class FoodItem
    {
        [Key]
        public int FoodItemID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [MaxLength(50)]
        public string Category { get; set; } // e.g., "Fast Food", "Combo", etc.

        [MaxLength(500)]
        public string Ingredients { get; set; }

        public string ImageURL { get; set; }

        // Navigation Properties
        public ICollection<ComboItem> ComboItems { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
