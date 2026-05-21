// FÁJL HELYE: Services/IOrderHandlingService.cs

using System.Collections.Generic;
using TechShop.Models;

namespace TechShop.Services
{
    public interface IOrderHandlingService
    {
        List<Order> GetAllOrders();
        Order GetOrderDetails(int id);
        void UpdateOrderStatus(int id, string newStatus);
    }
}