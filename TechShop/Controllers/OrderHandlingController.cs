
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechShop.Services; 
namespace TechShop.Controllers
{
    [Authorize]
    public class OrderHandlingController : Controller
    {
        private readonly IOrderHandlingService _orderService;

                public OrderHandlingController(IOrderHandlingService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            var orders = _orderService.GetAllOrders();
            return View(orders);
        }

        public IActionResult Details(int id)
        {
            var order = _orderService.GetOrderDetails(id);

            if (order == null) return NotFound();
            return View(order);
        }

        [HttpPost]
        public IActionResult UpdateStatus(int id, string newStatus)
        {
            _orderService.UpdateOrderStatus(id, newStatus);

            TempData["SuccessMessage"] = $"A rendelés új státusza: {newStatus}";
            return RedirectToAction(nameof(Details), new { id = id });
        }
    }
}