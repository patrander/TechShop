

using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TechShop.DAL;
using TechShop.Models;

namespace TechShop.Services
{
    public class ProductAdminService : IProductAdminService
    {
        private readonly TechShopIdentityDbContext _context;

        public ProductAdminService(TechShopIdentityDbContext context)
        {
            _context = context;
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.Include(p => p.Category).ToList();
        }

        public List<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }

        public Product GetProductById(int id)
        {
            return _context.Products.Find(id);
        }

        public Product GetProductWithCategoryById(int id)
        {
            return _context.Products.Include(p => p.Category).FirstOrDefault(m => m.ProductId == id);
        }

        public void CreateProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            _context.Update(product);
            _context.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }
    }
}