using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechShop.DAL;

namespace TechShop.Controllers
{
    [Authorize]
    public class OrderHandlingController : Controller
    {
        private readonly TechShopIdentityDbContext _context;

        public OrderHandlingController(TechShopIdentityDbContext context)
        {
            _context = context;
        }

        // Főoldal: Rendelések listája (legújabbtól lefelé)
        public IActionResult Index()
        {
            var orders = _context.Orders.OrderByDescending(o => o.OrderDate).ToList();
            return View(orders);
        }

        // Részletek megtekintése (itt látja az admin, mit vettek pontosan)
        public IActionResult Details(int id)
        {
            var order = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(i => i.Product) // Behúzzuk a termékeket is
                .FirstOrDefault(o => o.OrderId == id);

            if (order == null) return NotFound();
            return View(order);
        }

        // Státusz frissítése
        [HttpPost]
        public IActionResult UpdateStatus(int id, string newStatus)
        {
            var order = _context.Orders.Find(id);
            if (order != null)
            {
                order.Status = newStatus;
                _context.SaveChanges();
            }
            TempData["SuccessMessage"] = $"A rendelés új státusza: {newStatus}";
            return RedirectToAction(nameof(Details), new { id = id });
        }
    }
}