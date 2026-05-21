// FÁJL HELYE: Controllers/HomeController.cs

using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TechShop.Services;

namespace TechShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductCatalogService _productService;

        // Csak az IProductCatalogService-től függünk (Dependency Inversion)
        public HomeController(IProductCatalogService productService)
        {
            _productService = productService;
        }

        public IActionResult Index(string searchString, int? categoryId, string sortOrder)
        {
            // 1. Kategóriák lekérése
            ViewBag.Categories = _productService.GetAllCategories();

            // 2. Szűrt és rendezett termékek lekérése az üzleti rétegből
            var finalProducts = _productService.GetFilteredProducts(searchString, categoryId, sortOrder);

            // 3. UI állapot megőrzése
            ViewBag.CurrentSearch = searchString;
            ViewBag.CurrentCategory = categoryId;
            ViewBag.CurrentSort = sortOrder;

            // 4. Értesítés, ha nincs találat
            if (!finalProducts.Any() && !string.IsNullOrEmpty(searchString))
            {
                TempData["InfoMessage"] = $"Nincs találat a(z) '{searchString}' keresésre.";
            }

            return View(finalProducts);
        }
    }
}