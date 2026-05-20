using System.ComponentModel.DataAnnotations;

namespace TechShop.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "A kategória nevének megadása kötelező!")]
        [StringLength(50)]
        [Display(Name = "Kategória neve")]
        public string Name { get; set; }

        // Navigációs tulajdonság: Egy kategóriához több termék is tartozhat
        public virtual ICollection<Product> Products { get; set; }
    }
}