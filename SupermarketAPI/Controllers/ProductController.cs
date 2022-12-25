using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupermarketAPI.Data;

namespace SupermarketAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly SupermarketAPIContext _context;
        public ProductController(SupermarketAPIContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
            if (!_context.ProductCategory.Any())
            {
                ProductCategory phones = new ProductCategory { Name = "Телефони" };
                ProductCategory tablets = new ProductCategory { Name = "Планшети" };
                ProductCategory laptops = new ProductCategory { Name = "Ноутбуки" };
                ProductCategory monitor = new ProductCategory { Name = "Монітори" };
                Product user1 = new Product { Name = "iPhone 12", ProductCategory = phones, Price = 23000, PhotoUrl = "https://d1eh9yux7w8iql.cloudfront.net/product_images/None_bb1e34e0-4112-46f4-b7e0-7394beba3f40.jpg" };
                Product user2 = new Product { Name = "iPhone 13", ProductCategory = phones, Price = 25999, PhotoUrl = "https://cdn.lesnumeriques.com/optim/product/63/63483/b690f827-iphone-13__450_400.jpeg" };
                Product user3 = new Product { Name = "Macbook Pro 13", ProductCategory = laptops, Price = 42999, PhotoUrl = "https://jabko.ua/image/cache/catalog/products/2020/11/111232/4%20(1)-1397x1397.jpg" };
                Product user4 = new Product { Name = "Macbook Pro 16", ProductCategory = laptops, Price = 51999, PhotoUrl = "https://hotline.ua/img/tx/212/2127799955.jpg" };
                Product user5 = new Product { Name = "Lenovo IdeaPad 2", ProductCategory = laptops, Price = 9999, PhotoUrl = "https://hotline.ua/img/tx/113/11336895.jpg" };
                Product user6 = new Product { Name = "Samsung Galaxy Pad", ProductCategory = tablets, Price = 8999, PhotoUrl = "https://i.allo.ua/media/catalog/product/cache/1/image/710x600/602f0fa2c1f0d1ba5e241f914e856ff9/1/4/145197-samsung-tab-a8-10-5-lte-3-32gb-dark-grey_1.jpg" };
                Product user7 = new Product { Name = "iPad Air 2022", ProductCategory = tablets, Price = 35999, PhotoUrl = "https://jabko.ua/image/cache/catalog/products/2022/03/082332/ipad-air-select-wifi-purple-2022%20(1)-max-420.jpg" };
                Product user8 = new Product { Name = "Benq 144 Ggz", ProductCategory = monitor, Price = 12999, PhotoUrl = "https://content1.rozetka.com.ua/goods/images/big_tile/299048429.jpg" };
                _context.ProductCategory.AddRange(phones, laptops, tablets, monitor);
                _context.Product.AddRange(user1, user2, user3, user4, user5, user6, user7, user8);
                _context.SaveChanges();
            }
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
          if (_context.Product == null)
          {
              return NotFound();
          }
            return await _context.Product.Include("ProductCategory").ToListAsync();
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
          if (_context.Product == null)
          {
              return NotFound();
          }

          var product = await _context.Product.Include("ProductCategory").FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Product/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Product
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
          if (_context.Product == null)
          {
              return Problem("Entity set 'SupermarketAPIContext.Product'  is null.");
          }
            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_context.Product == null)
            {
                return NotFound();
            }
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return (_context.Product?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
