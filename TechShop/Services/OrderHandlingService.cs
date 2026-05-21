
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

                public OrderHandlingService(TechShopIdentityDbContext context)
        {
            _context = context;
        }

        public List<Order> GetAllOrders()
        {
                        return _context.Orders.OrderByDescending(o => o.OrderDate).ToList();
        }

        public Order GetOrderDetails(int id)
        {
                        return _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(o => o.OrderId == id);
        }

        public void UpdateOrderStatus(int id, string newStatus)
        {
                        var order = _context.Orders.Find(id);
            if (order != null)
            {
                order.Status = newStatus;
                _context.SaveChanges();
            }
        }
    }
}