// FÁJL HELYE: Services/ICartService.cs

using System.Collections.Generic;
using TechShop.Models; // Ide jönnek a modellek (Product, OrderItem)

namespace TechShop.Services
{
    public interface ICartService
    {
        void AddToCart(int productId);
        void RemoveFromCart(int productId);
        void ClearCart();
        List<OrderItem> GetCartItems();
        decimal GetTotal();
    }
}