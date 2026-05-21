using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechShop.Models;
using TechShop.Services;

namespace TechShop.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ICheckoutService _checkoutService;

        public CartController(ICartService cartService, ICheckoutService checkoutService)
        {
            _cartService = cartService;
            _checkoutService = checkoutService;
        }

    
        public IActionResult Index()
        {
            var cart = _cartService.GetCartItems();
            ViewBag.cart = cart;
            ViewBag.total = _cartService.GetTotal();
            return View();
        }

       
        [Route("buy/{id}")] 
        public IActionResult Buy(int id)
        {
            var productName = _cartService.AddToCart(id);
            if (productName != null)
            {
                TempData["SuccessMessage"] = $"{productName} sikeresen a kosaradba került!";
            }
            return RedirectToAction("Index");
        }

                [Route("remove/{id}")]         public IActionResult Remove(int id)
        {
            _cartService.RemoveFromCart(id);
            return RedirectToAction("Index");
        }

                [Authorize]
        public IActionResult Checkout()
        {
            var cart = _cartService.GetCartItems();
            if (cart == null || cart.Count == 0) return RedirectToAction("Index");

            ViewBag.cart = cart;
            ViewBag.total = _cartService.GetTotal();
            return View();
        }

                [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Checkout(Order order)
        {
            if (ModelState.IsValid)
            {
                var cart = _cartService.GetCartItems();
                if (cart == null || cart.Count == 0) return RedirectToAction("Index");

                bool success = _checkoutService.ProcessCheckout(order);

                if (success)
                {
                                        return View("Checkout2", order);
                }
            }

            return View(order);
        }
    }
}