

using System.Collections.Generic;
using TechShop.Models; 

namespace TechShop.Services
{
    public interface IProductCatalogService
    {
        List<Category> GetAllCategories();
        List<Product> GetFilteredProducts(string searchString, int? categoryId, string sortOrder);
    }
}