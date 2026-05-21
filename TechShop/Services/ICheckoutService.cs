

using TechShop.Models; 

namespace TechShop.Services
{
    public interface ICheckoutService
    {
        bool ProcessCheckout(Order order);
    }
}