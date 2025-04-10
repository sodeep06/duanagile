using System.ComponentModel.DataAnnotations;

namespace duanagile.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } // Possible values: "Pending", "In Progress", "Delivered"

        // Navigation Properties
        public User User { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
