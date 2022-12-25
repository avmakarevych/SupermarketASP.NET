using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SupermarketAPI;

namespace SupermarketAPI.Data
{
    public class SupermarketAPIContext : DbContext
    {
        public SupermarketAPIContext (DbContextOptions<SupermarketAPIContext> options)
            : base(options)
        {
        }

        public DbSet<SupermarketAPI.Product> Product { get; set; } = default!;
        public DbSet<SupermarketAPI.ProductCategory> ProductCategory { get; set; } = default!;
    }
}
