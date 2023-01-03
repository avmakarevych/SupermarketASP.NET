using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Supermarket.Models;

namespace Supermarket.Data;

public class ApplicationDbContext : IdentityDbContext
{
   public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
   			: base(options)
   		{

	        //Database.EnsureDeleted();
   			Database.EnsureCreated();
			
   			if (!ProductCategory.Any())
           {
               ProductCategory phones = new ProductCategory { Name = "Телефони" };
               ProductCategory tablets = new ProductCategory { Name = "Планшети" };
               ProductCategory laptops = new ProductCategory { Name = "Ноутбуки" };
               ProductCategory monitor = new ProductCategory { Name = "Монітори" };
               Product user1 = new Product { Name = "iPhone 12", ProductCategory = phones, Price = 23000, PhotoUrl = "https://d1eh9yux7w8iql.cloudfront.net/product_images/None_bb1e34e0-4112-46f4-b7e0-7394beba3f40.jpg"};
               Product user2 = new Product { Name = "iPhone 13", ProductCategory = phones, Price = 25999, PhotoUrl = "https://cdn.lesnumeriques.com/optim/product/63/63483/b690f827-iphone-13__450_400.jpeg" };
               Product user3 = new Product { Name = "Macbook Pro 13", ProductCategory = laptops, Price = 42999, PhotoUrl = "https://jabko.ua/image/cache/catalog/products/2020/11/111232/4%20(1)-1397x1397.jpg" };
               Product user4 = new Product { Name = "Macbook Pro 16", ProductCategory = laptops, Price = 51999, PhotoUrl = "https://hotline.ua/img/tx/212/2127799955.jpg" };
               Product user5 = new Product { Name = "Lenovo IdeaPad 2", ProductCategory = laptops, Price = 9999, PhotoUrl = "https://hotline.ua/img/tx/113/11336895.jpg" };
               Product user6 = new Product { Name = "Samsung Galaxy Pad", ProductCategory = tablets, Price = 8999, PhotoUrl = "https://i.allo.ua/media/catalog/product/cache/1/image/710x600/602f0fa2c1f0d1ba5e241f914e856ff9/1/4/145197-samsung-tab-a8-10-5-lte-3-32gb-dark-grey_1.jpg" };
               Product user7 = new Product { Name = "iPad Air 2022", ProductCategory = tablets, Price = 35999, PhotoUrl = "https://jabko.ua/image/cache/catalog/products/2022/03/082332/ipad-air-select-wifi-purple-2022%20(1)-max-420.jpg" };
               Product user8 = new Product { Name = "Benq 144 Ggz", ProductCategory = monitor, Price = 12999, PhotoUrl = "https://content1.rozetka.com.ua/goods/images/big_tile/299048429.jpg" };
   
               ProductCategory.AddRange(phones, laptops, tablets, monitor);
               Product.AddRange(user1, user2, user3, user4, user5, user6, user7, user8);
               SaveChanges();
           }
            
   		}
	   protected override void OnModelCreating(ModelBuilder modelBuilder)
	   {
		   base.OnModelCreating(modelBuilder);
		   modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" });
	   }

   		public DbSet<Product> Product { get; set; } = default!;
   		public DbSet<ProductCategory> ProductCategory { get; set; } = default!;
}