using Microsoft.EntityFrameworkCore;
using ProductApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApi.Data {
    public class ProductsDbContext : DbContext {
        public ProductsDbContext(DbContextOptions<ProductsDbContext> options):base(options) {
        }
        public DbSet<Product> Products { get; set; }
    }
}
