
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechShop.Models;
using TechShop.Services; 
namespace TechShop.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductAdminService _adminService;

                public ProductsController(IProductAdminService adminService)
        {
            _adminService = adminService;
        }

                public IActionResult Index()
        {
            var products = _adminService.GetAllProducts();
            return View(products);
        }

                public IActionResult Create()
        {
            ViewBag.Categories = _adminService.GetAllCategories();
            return View();
        }

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

                public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var product = _adminService.GetProductById(id.Value);
            if (product == null) return NotFound();

            ViewBag.Categories = _adminService.GetAllCategories();
            return View(product);
        }

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

                public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var product = _adminService.GetProductWithCategoryById(id.Value);
            if (product == null) return NotFound();

            return View(product);
        }

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