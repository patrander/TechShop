// FÁJL HELYE: Services/OrderHandlingService.cs

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TechShop.DAL;
using TechShop.Models;

namespace TechShop.Services
{
    public class OrderHandlingService : IOrderHandlingService
    {
        private readonly TechShopIdentityDbContext _context;

        // Dependency Injection-nel megkapjuk az adatbázist
        public OrderHandlingService(TechShopIdentityDbContext context)
        {
            _context = context;
        }

        public List<Order> GetAllOrders()
        {
            // A legújabb rendelések listázása
            return _context.Orders.OrderByDescending(o => o.OrderDate).ToList();
        }

        public Order GetOrderDetails(int id)
        {
            // A részletes lekérdezés a termékekkel együtt
            return _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(o => o.OrderId == id);
        }

        public void UpdateOrderStatus(int id, string newStatus)
        {
            // Státusz frissítése és mentés
            var order = _context.Orders.Find(id);
            if (order != null)
            {
                order.Status = newStatus;
                _context.SaveChanges();
            }
        }
    }
}