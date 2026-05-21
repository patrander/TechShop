using Microsoft.EntityFrameworkCore;
using TechShop.DAL;

namespace TechShop.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            TechShopIdentityDbContext context = app.ApplicationServices.
                CreateScope().ServiceProvider.GetRequiredService<TechShopIdentityDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category { Name = "Laptopok" },
                    new Category { Name = "Okostelefonok" },
                    new Category { Name = "Fejhallgatók" },
                    new Category { Name = "Okosórák" }
                );
                context.SaveChanges();
            }

            
            if (!context.Products.Any())
            {
                var laptopCategory = context.Categories.First(c => c.Name == "Laptopok");
                var phoneCategory = context.Categories.First(c => c.Name == "Okostelefonok");
                var audioCategory = context.Categories.First(c => c.Name == "Fejhallgatók");
                var watchCategory = context.Categories.First(c => c.Name == "Okosórák");

                context.Products.AddRange(
                    new Product { Name = "ProBook G9", Description = "Nagy teljesítményű üzleti laptop mindennapi munkához.", Price = 350000, Category = laptopCategory },
                    new Product { Name = "MacBook Air M2", Description = "Ultravékony, könnyű és villámgyors Apple laptop.", Price = 480000, Category = laptopCategory },
                    new Product { Name = "Lenovo Legion 5", Description = "Prémium gamer laptop brutális grafikus teljesítménnyel.", Price = 520000, Category = laptopCategory },

                    new Product { Name = "iPhone 15 Pro", Description = "Titán dizájn, kiváló kamera és A17 Pro chip.", Price = 490000, Category = phoneCategory },
                    new Product { Name = "Samsung Galaxy S24", Description = "A legújabb AI funkciókkal ellátott Android csúcsmobil.", Price = 380000, Category = phoneCategory },

                    new Product { Name = "Sony WH-1000XM5", Description = "Iparágvezető zajszűrős Bluetooth fejhallgató audiofileknek.", Price = 135000, Category = audioCategory },
                    new Product { Name = "Apple AirPods Pro 2", Description = "Kiváló hangzás és aktív zajszűrés egy apró fülhallgatóban.", Price = 110000, Category = audioCategory },

                    new Product { Name = "Garmin Fenix 7", Description = "Profi multisport okosóra extrém üzemidővel.", Price = 250000, Category = watchCategory },
                    new Product { Name = "Apple Watch Series 9", Description = "A tökéletes társ az egészség és fitnesz követéséhez.", Price = 175000, Category = watchCategory }
                );
                context.SaveChanges();
            }
        }
    }
}