
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TechShop.DAL;
using TechShop.Models;

namespace TechShop.Services
{
    public class ProductCatalogService : IProductCatalogService
    {
        private readonly TechShopIdentityDbContext _context;

        public ProductCatalogService(TechShopIdentityDbContext context)
        {
            _context = context;
        }

        public List<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }

        public List<Product> GetFilteredProducts(string searchString, int? categoryId, string sortOrder)
        {
            var products = _context.Products.Include(p => p.Category).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Name.Contains(searchString));
            }

            if (categoryId.HasValue)
            {
                products = products.Where(p => p.CategoryId == categoryId.Value);
            }

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
                    products = products.OrderBy(p => p.Name);
                    break;
            }

            return products.ToList();
        }
    }
}