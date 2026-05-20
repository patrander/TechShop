using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TechShop.DAL;
using TechShop.Models;

namespace TechShop.Controllers
{
    public class CartController : Controller
    {
        // ITT JAVÍTVA A TE ADATBÁZIS KONTEXTUSOD NEVÉRE:
        private readonly TechShopIdentityDbContext _context;

        public CartController(TechShopIdentityDbContext context)
        {
            _context = context;
        }

        // --- KOSÁR MEGJELENÍTÉSE ---
        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") ?? new List<Item>();
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Product.Price * item.Quantity);
            return View();
        }

        // --- TERMÉK KOSÁRBA TÉTELE ---
        [Route("buy/{id}")]
        

        public IActionResult Buy(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return RedirectToAction("Index", "Home");

            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") ?? new List<Item>();

            int index = isExist(id, cart);
            if (index != -1)
            {
                cart[index].Quantity++;
            }
            else
            {
                cart.Add(new Item { Product = product, Quantity = 1 });
            }

            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            TempData["SuccessMessage"] = $"{product.Name} sikeresen a kosaradba került!";
            return RedirectToAction("Index");
        }

        // --- TERMÉK TÖRLÉSE A KOSÁRBÓL ---
        [Route("remove/{id}")]
        public IActionResult Remove(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            if (cart != null)
            {
                int index = isExist(id, cart);
                if (index != -1)
                {
                    cart.RemoveAt(index);
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }

        // --- SEGÉDMETÓDUS ---
        private int isExist(int id, List<Item> cart)
        {
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.ProductId == id) return i;
            }
            return -1;
        }

        // --- PÉNZTÁR (GET) - Űrlap megjelenítése ---
        [Authorize]
        public IActionResult Checkout()
        {
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            if (cart == null || cart.Count == 0) return RedirectToAction("Index");

            // Átadjuk az adatokat a nézetnek, hogy lássa mit vesz!
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Product.Price * item.Quantity);

            return View();
        }

        // --- PÉNZTÁR (POST) - Rendelés mentése az adatbázisba ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Checkout(Order order)
        {
            if (ModelState.IsValid)
            {
                var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");

                if (cart == null || cart.Count == 0) return RedirectToAction("Index");

                // Rendelés alapadatai
                order.TotalPrice = (int)cart.Sum(item => item.Product.Price * item.Quantity);
                order.OrderDate = DateTime.Now;
                order.Status = "Feldolgozás alatt";
                order.OrderItems = new List<OrderItem>();

                // Kosár tételeinek átmásolása a rendelésbe
                foreach (var item in cart)
                {
                    order.OrderItems.Add(new OrderItem
                    {
                        ProductId = item.Product.ProductId,
                        Quantity = item.Quantity,
                        Price = (int)item.Product.Price
                    });
                }

                // Mentés az adatbázisba
                _context.Orders.Add(order);
                _context.SaveChanges();

                // Kosár ürítése sikeres rendelés után
                HttpContext.Session.Remove("cart");

                return View("Checkout2", order);
            }

            // Ha hiba volt, visszatöltjük az űrlapot
            return View(order);
        }
    }
}