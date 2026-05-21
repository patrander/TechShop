// FÁJL HELYE: Services/CartService.cs

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using TechShop.DAL;
using TechShop.Models; // Tartalmazza a Product, OrderItem és TechShopIdentityDbContext osztályokat
// Megjegyzés: Ha a TechShopIdentityDbContext pl. a TechShop.Data névtérben van, 
// akkor add hozzá a "using TechShop.Data;" sort is!

namespace TechShop.Services
{
    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly TechShopIdentityDbContext _context;
        private const string CartSessionKey = "TechShop_Cart";

        // Dependency Injection-nel bekérjük a HttpContext-et (a Session miatt) és az Adatbázist
        public CartService(IHttpContextAccessor httpContextAccessor, TechShopIdentityDbContext context)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // Segédtulajdonság a Session egyszerűbb eléréséhez
        private ISession Session => _httpContextAccessor.HttpContext?.Session;

        public List<OrderItem> GetCartItems()
        {
            if (Session == null) return new List<OrderItem>();

            var sessionData = Session.GetString(CartSessionKey);
            if (string.IsNullOrEmpty(sessionData))
            {
                return new List<OrderItem>();
            }
            return JsonSerializer.Deserialize<List<OrderItem>>(sessionData);
        }

        public void AddToCart(int productId)
        {
            if (Session == null) return;

            var cart = GetCartItems();
            var product = _context.Products.Find(productId);

            if (product != null)
            {
                var existingItem = cart.FirstOrDefault(c => c.ProductId == productId);
                if (existingItem != null)
                {
                    existingItem.Quantity++;
                }
                else
                {
                    cart.Add(new OrderItem
                    {
                        ProductId = product.ProductId,
                        Product = product,
                        Quantity = 1,
                        Price = (int)product.Price
                    });
                }

                Session.SetString(CartSessionKey, JsonSerializer.Serialize(cart));
            }
        }

        public void RemoveFromCart(int productId)
        {
            if (Session == null) return;

            var cart = GetCartItems();
            var item = cart.FirstOrDefault(c => c.ProductId == productId);
            if (item != null)
            {
                if (item.Quantity > 1)
                {
                    item.Quantity--;
                }
                else
                {
                    cart.Remove(item);
                }
                Session.SetString(CartSessionKey, JsonSerializer.Serialize(cart));
            }
        }

        public void ClearCart()
        {
            Session?.Remove(CartSessionKey);
        }

        public decimal GetTotal()
        {
            return GetCartItems().Sum(item => item.Price * item.Quantity);
        }
    }
}