// FÁJL HELYE: Services/IProductAdminService.cs

using System.Collections.Generic;
using TechShop.Models;

namespace TechShop.Services
{
    public interface IProductAdminService
    {
        List<Product> GetAllProducts();
        List<Category> GetAllCategories();
        Product GetProductById(int id);
        Product GetProductWithCategoryById(int id);
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
    }
}