// FÁJL HELYE: Controllers/ProductsController.cs

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechShop.Models;
using TechShop.Services; // Az új Service réteg importálása

namespace TechShop.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductAdminService _adminService;

        // Csak a Service-t injektáljuk, az adatbázist már nem!
        public ProductsController(IProductAdminService adminService)
        {
            _adminService = adminService;
        }

        // --- 1. TERMÉKEK LISTÁZÁSA ---
        public IActionResult Index()
        {
            var products = _adminService.GetAllProducts();
            return View(products);
        }

        // --- 2. ÚJ TERMÉK LÉTREHOZÁSA (GET) ---
        public IActionResult Create()
        {
            ViewBag.Categories = _adminService.GetAllCategories();
            return View();
        }

        // --- 3. ÚJ TERMÉK LÉTREHOZÁSA (POST) ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            ModelState.Remove("Category");

            if (ModelState.IsValid)
            {
                _adminService.CreateProduct(product);
                TempData["SuccessMessage"] = $"{product.Name} sikeresen hozzáadva!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = _adminService.GetAllCategories();
            return View(product);
        }

        // --- 4. TERMÉK SZERKESZTÉSE (GET) ---
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var product = _adminService.GetProductById(id.Value);
            if (product == null) return NotFound();

            ViewBag.Categories = _adminService.GetAllCategories();
            return View(product);
        }

        // --- 5. TERMÉK SZERKESZTÉSE (POST) ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product product)
        {
            if (id != product.ProductId) return NotFound();

            ModelState.Remove("Category");

            if (ModelState.IsValid)
            {
                _adminService.UpdateProduct(product);
                TempData["SuccessMessage"] = "A termék adatai sikeresen frissítve!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = _adminService.GetAllCategories();
            return View(product);
        }

        // --- 6. TERMÉK TÖRLÉSE (GET) ---
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var product = _adminService.GetProductWithCategoryById(id.Value);
            if (product == null) return NotFound();

            return View(product);
        }

        // --- 7. TERMÉK TÖRLÉSE (POST) ---
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _adminService.DeleteProduct(id);

            TempData["SuccessMessage"] = "A termék véglegesen törölve lett.";
            return RedirectToAction(nameof(Index));
        }
    }
}