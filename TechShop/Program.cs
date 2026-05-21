using Microsoft.EntityFrameworkCore;
using TechShop.DAL;
using TechShop.Models;
using Microsoft.AspNetCore.Identity;
using TechShop.Services;
namespace TechShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<TechShopIdentityDbContext>(opts => {
                opts.UseSqlServer(builder.Configuration.GetConnectionString("TechShopConnection"));
            });

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false).AddRoles<IdentityRole>().AddEntityFrameworkStores<TechShopIdentityDbContext>();
            builder.Services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            builder.Services.AddHttpContextAccessor(); 
            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddScoped<IProductCatalogService, ProductCatalogService>();
            builder.Services.AddScoped<IProductAdminService, ProductAdminService>();
            builder.Services.AddScoped<IOrderHandlingService, OrderHandlingService>();
            builder.Services.AddScoped<ICheckoutService, CheckoutService>(); 


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
            app.MapRazorPages();

            SeedData.EnsurePopulated(app);
            app.Run();
        }
    }
}
