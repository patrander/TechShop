using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechShop.DAL;
using TechShop.Models;

namespace TechShop.Controllers
{
    // Ezzel biztosítjuk, hogy csak bejelentkezett felhasználók (a mi esetünkben az "adminok") 
    // férjenek hozzá a termékek módosításához
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly TechShopIdentityDbContext _context;

        public ProductsController(TechShopIdentityDbContext context)
        {
            _context = context;
        }

        // --- 1. TERMÉKEK LISTÁZÁSA ---
        public IActionResult Index()
        {
            // Az Include behozza a kategória adatait is, így tudjuk a nevét kiírni a táblázatban
            var products = _context.Products.Include(p => p.Category).ToList();
            return View(products);
        }

        // --- 2. ÚJ TERMÉK LÉTREHOZÁSA (Űrlap megjelenítése) ---
        public IActionResult Create()
        {
            // Betöltjük a kategóriákat a legördülő menühöz
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

        // --- 3. ÚJ TERMÉK LÉTREHOZÁSA (Adatok mentése az adatbázisba) ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            // KIVÉTEL: A kategória objektumot nem az űrlap küldi (csak az ID-t), 
            // ezért kivesszük a kötelező ellenőrzésből, különben a ModelState invalid lenne!
            ModelState.Remove("Category");

            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                TempData["SuccessMessage"] = $"{product.Name} sikeresen hozzáadva!";
                return RedirectToAction(nameof(Index)); // Siker esetén vissza a listára
            }

            // Ha hiba volt (pl. rossz ár), újra fel kell tölteni a kategóriákat, mielőtt visszadobjuk az űrlapot!
            ViewBag.Categories = _context.Categories.ToList();
            return View(product);
        }

        // --- 4. TERMÉK SZERKESZTÉSE (Űrlap megjelenítése) ---
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            ViewBag.Categories = _context.Categories.ToList();
            return View(product);
        }

        // --- 5. TERMÉK SZERKESZTÉSE (Adatok frissítése) ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product product)
        {
            if (id != product.ProductId) return NotFound();

            // Itt is ki kell venni a kategóriát a validációból
            ModelState.Remove("Category");

            if (ModelState.IsValid)
            {
                _context.Update(product);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "A termék adatai sikeresen frissítve!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = _context.Categories.ToList();
            return View(product);
        }

        // --- 6. TERMÉK TÖRLÉSE (Biztonsági kérdés megjelenítése) ---
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var product = _context.Products.Include(p => p.Category).FirstOrDefault(m => m.ProductId == id);
            if (product == null) return NotFound();

            return View(product);
        }

        // --- 7. TERMÉK TÖRLÉSE (Végleges törlés az adatbázisból) ---
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            TempData["SuccessMessage"] = "A termék véglegesen törölve lett.";
            return RedirectToAction(nameof(Index));
        }
    }
}