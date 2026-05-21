// FÁJL HELYE: Controllers/CartController.cs

using Microsoft.AspNetCore.Mvc;
using TechShop.Services; // Beimportáljuk az új Service rétegünket

namespace TechShop.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        // A kontroller már nem látja az adatbázist és a Session-t sem, csak az Interfészt!
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // Kosár oldal (Index)
        public IActionResult Index()
        {
            ViewBag.Total = _cartService.GetTotal();
            var items = _cartService.GetCartItems();
            return View(items);
        }

        // Termék hozzáadása a kosárhoz
        public IActionResult Add(int id)
        {
            _cartService.AddToCart(id);
            TempData["Success"] = "A termék sikeresen a kosárba került!";
            return RedirectToAction("Index", "Home");
        }

        // Mennyiség csökkentése / Eltávolítás
        public IActionResult Remove(int id)
        {
            _cartService.RemoveFromCart(id);
            TempData["Info"] = "Kosár frissítve.";
            return RedirectToAction("Index");
        }

        // Kosár teljes kiürítése
        public IActionResult Clear()
        {
            _cartService.ClearCart();
            TempData["Info"] = "A kosár kiürítve.";
            return RedirectToAction("Index");
        }
    }
}