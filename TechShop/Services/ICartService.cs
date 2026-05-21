using System.Collections.Generic;
using TechShop.Models;

namespace TechShop.Services
{
    public interface ICartService
    {
        List<Item> GetCartItems();
        string AddToCart(int productId); // Visszaadja a termék nevét az üzenethez
        void RemoveFromCart(int productId);
        void ClearCart();
        decimal GetTotal();
    }
}