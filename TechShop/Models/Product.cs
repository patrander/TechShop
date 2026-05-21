using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechShop.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "A termék nevének megadása kötelező!")]
        [StringLength(100)]
        [Display(Name = "Termék neve")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A leírás kötelező!")]
        [Display(Name = "Leírás")]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Range(1, 10000000, ErrorMessage = "Az árnak 1 és 10.000.000 Ft között kell lennie.")]
        [Display(Name = "Ár (Ft)")]
        public decimal Price { get; set; }

        
        [Required(ErrorMessage = "Válassz kategóriát!")]
        [Display(Name = "Kategória")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}