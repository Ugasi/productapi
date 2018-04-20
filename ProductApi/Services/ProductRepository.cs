using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductApi.Data;
using ProductApi.Models;

namespace ProductApi.Services
{
    public class ProductRepository : IProduct {

        private ProductsDbContext productsDbContext;

        public ProductRepository(ProductsDbContext productsDbContext) {
            this.productsDbContext = productsDbContext;
        }

        public void AddProduct(Product product) {
            productsDbContext.Products.Add(product);
            productsDbContext.SaveChanges(true);
        }

        public void DeleteProduct(int id) {
            Product product = productsDbContext.Products.Find(id);
            productsDbContext.Products.Remove(product);
            productsDbContext.SaveChanges(true);
        }

        public Product GetProduct(int id) {
            return productsDbContext.Products.SingleOrDefault(m => m.Id == id);
        }

        public IQueryable<Product> GetProducts() {
            return productsDbContext.Products;
        }

        public void UpdateProduct(Product product) {
            productsDbContext.Products.Update(product);
            productsDbContext.SaveChanges(true);
        }
    }
}
