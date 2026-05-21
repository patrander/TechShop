// FÁJL HELYE: Services/IProductCatalogService.cs

using System.Collections.Generic;
using TechShop.Models; // Ide mutat a Category és Product modelled

namespace TechShop.Services
{
    public interface IProductCatalogService
    {
        List<Category> GetAllCategories();
        List<Product> GetFilteredProducts(string searchString, int? categoryId, string sortOrder);
    }
}