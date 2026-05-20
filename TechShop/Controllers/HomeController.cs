using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechShop.DAL;
using TechShop.Models;

namespace TechShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly TechShopIdentityDbContext _context;

        public HomeController(TechShopIdentityDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searchString, int? categoryId, string sortOrder)
        {
            // 1. Kategóriák lekérése a legördülő menühöz
            ViewBag.Categories = _context.Categories.ToList();

            // 2. Alap lekérdezés indítása (Minden termék a kategóriájával együtt)
            var products = _context.Products.Include(p => p.Category).AsQueryable();

            // 3. SZŰRÉS: Szöveges keresés név alapján
            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Name.Contains(searchString));
            }

            // 4. SZŰRÉS: Kategória alapján (ha ki van választva konkrét kategória)
            if (categoryId.HasValue)
            {
                products = products.Where(p => p.CategoryId == categoryId.Value);
            }

            // 5. RENDEZÉS: Ár és név szerint
            switch (sortOrder)
            {
                case "price_asc":
                    products = products.OrderBy(p => p.Price);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(p => p.Price);
                    break;
                case "name_desc":
                    products = products.OrderByDescending(p => p.Name);
                    break;
                default:
                    products = products.OrderBy(p => p.Name); // Alapértelmezett: Név szerint A-Z
                    break;
            }

            // Mentjük az aktuális értékeket, hogy a felület "emlékezzen" rájuk frissítés után
            ViewBag.CurrentSearch = searchString;
            ViewBag.CurrentCategory = categoryId;
            ViewBag.CurrentSort = sortOrder;

            var finalProducts = products.ToList();
            if (!finalProducts.Any() && !string.IsNullOrEmpty(searchString))
            {
                TempData["InfoMessage"] = $"Nincs találat a(z) '{searchString}' keresésre.";
            }

            return View(finalProducts);
        }
    }
}