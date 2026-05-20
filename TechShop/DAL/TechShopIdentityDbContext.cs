using Microsoft.AspNetCore.Identity; // Ez kell az Identity-hez
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // Ez is
using Microsoft.EntityFrameworkCore;
using TechShop.Models;

namespace TechShop.DAL
{
    // Itt a DbContext-et átírjuk IdentityDbContext-re
    public class TechShopIdentityDbContext : IdentityDbContext<IdentityUser>
    {
        public TechShopIdentityDbContext(DbContextOptions<TechShopIdentityDbContext> options) : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}