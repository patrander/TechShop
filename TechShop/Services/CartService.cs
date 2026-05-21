using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using TechShop.DAL;
using TechShop.Models;

namespace TechShop.Services
{
    public class CartService : ICartService
    {
        private readonly TechShopIdentityDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartService(TechShopIdentityDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private ISession Session => _httpContextAccessor.HttpContext?.Session;

        public List<Item> GetCartItems()
        {
            return SessionHelper.GetObjectFromJson<List<Item>>(Session, "cart") ?? new List<Item>();
        }

        public string AddToCart(int productId)
        {
            var product = _context.Products.Find(productId);
            if (product == null) return null;

            var cart = GetCartItems();
            
            var existingItem = cart.FirstOrDefault(i => i.Product.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                cart.Add(new Item { Product = product, Quantity = 1 });
            }

            SessionHelper.SetObjectAsJson(Session, "cart", cart);
            return product.Name;
        }

        public void RemoveFromCart(int productId)
        {
            var cart = GetCartItems();
            var item = cart.FirstOrDefault(i => i.Product.ProductId == productId);

            if (item != null)
            {
                cart.Remove(item);
                SessionHelper.SetObjectAsJson(Session, "cart", cart);
            }
        }

        public void ClearCart()
        {
            Session?.Remove("cart");
        }

        public decimal GetTotal()
        {
            return GetCartItems().Sum(item => item.Product.Price * item.Quantity);
        }
    }
}