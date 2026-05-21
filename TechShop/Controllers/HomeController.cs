
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TechShop.Services;

namespace TechShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductCatalogService _productService;

                public HomeController(IProductCatalogService productService)
        {
            _productService = productService;
        }

        public IActionResult Index(string searchString, int? categoryId, string sortOrder)
        {
                        ViewBag.Categories = _productService.GetAllCategories();

                        var finalProducts = _productService.GetFilteredProducts(searchString, categoryId, sortOrder);

                        ViewBag.CurrentSearch = searchString;
            ViewBag.CurrentCategory = categoryId;
            ViewBag.CurrentSort = sortOrder;

                        if (!finalProducts.Any() && !string.IsNullOrEmpty(searchString))
            {
                TempData["InfoMessage"] = $"Nincs találat a(z) '{searchString}' keresésre.";
            }

            return View(finalProducts);
        }
    }
}