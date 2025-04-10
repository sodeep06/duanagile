using System.ComponentModel.DataAnnotations;

namespace duanagile.Models{

    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [Required]
        [MaxLength(20)]
        public string Role { get; set; }  // Possible values: "Admin", "Customer", "Guest"

        // Navigation Properties
        public ICollection<Order> Orders { get; set; }
    }
}
