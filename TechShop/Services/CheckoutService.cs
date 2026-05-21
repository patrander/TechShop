using System;
using System.Collections.Generic;
using System.Linq;
using TechShop.DAL;
using TechShop.Models;

namespace TechShop.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly TechShopIdentityDbContext _context;
        private readonly ICartService _cartService;

        public CheckoutService(TechShopIdentityDbContext context, ICartService cartService)
        {
            _context = context;
            _cartService = cartService;
        }

        public bool ProcessCheckout(Order order)
        {
            var cart = _cartService.GetCartItems();
            if (cart == null || cart.Count == 0) return false;

            
            order.TotalPrice = (int)cart.Sum(item => item.Product.Price * item.Quantity);
            order.OrderDate = DateTime.Now;
            order.Status = "Feldolgozás alatt";
            order.OrderItems = new List<OrderItem>();

            foreach (var item in cart)
            {
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = item.Product.ProductId,
                    Quantity = item.Quantity,
                    Price = (int)item.Product.Price
                });
            }

            _context.Orders.Add(order);
            _context.SaveChanges();

            _cartService.ClearCart();
            return true;
        }
    }
}