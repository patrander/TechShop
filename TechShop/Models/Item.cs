namespace TechShop.Models
{
    public class Item
    {
        public Product Product { get; set; } // A könyv helyett mi terméket használunk
        public int Quantity { get; set; }
    }
}