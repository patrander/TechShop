// FÁJL HELYE: Services/ICheckoutService.cs

using TechShop.Models; // Vagy TechShop.DAL, ahol az Order modelled van

namespace TechShop.Services
{
    public interface ICheckoutService
    {
        bool ProcessCheckout(Order order);
    }
}