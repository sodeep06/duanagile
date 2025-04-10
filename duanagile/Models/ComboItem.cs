using static Azure.Core.HttpHeader;
using System.ComponentModel.DataAnnotations;

namespace duanagile.Models
{
    public class ComboItem
    {
        [Key]
        public int ComboItemID { get; set; }

        [Required]
        public int ComboID { get; set; }

        [Required]
        public int FoodItemID { get; set; }

        // Navigation Properties
        public Combo Combo { get; set; }
        public FoodItem FoodItem { get; set; }
    }
}
