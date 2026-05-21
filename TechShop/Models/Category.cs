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

        
        public virtual ICollection<Product> Products { get; set; }
    }
}