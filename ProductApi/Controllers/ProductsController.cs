using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Data;
using ProductApi.Models;

namespace ProductApi.Controllers {
    [Route("api/Products")]
    public class ProductsController : Controller {

        ProductsDbContext productsDbContext;

        public ProductsController(ProductsDbContext productsDbContext) {
            this.productsDbContext = productsDbContext;
        }

        [HttpGet]
        public IActionResult Get(string sortPrice) {
            IQueryable<Product> products = GetProductsSorted(sortPrice);
            return StatusCode(StatusCodes.Status200OK, products); ;
        }

        private IQueryable<Product> GetProductsSorted(string sortPrice) {
            IQueryable<Product> products;
            switch (sortPrice) {
                case "desc":
                    products = productsDbContext.Products.OrderByDescending(p => p.Price);
                    break;
                case "asc":
                    products = productsDbContext.Products.OrderBy(p => p.Price);
                    break;
                default:
                    products = productsDbContext.Products;
                    break;
            }

            return products;
        }

        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id) {
            Product product = productsDbContext.Products.SingleOrDefault(m => m.Id == id);
            if (product == null) {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return StatusCode(StatusCodes.Status200OK, product);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Product product) {
            if (ModelState.IsValid) {
                productsDbContext.Products.Add(product);
                productsDbContext.SaveChanges(true);
                return StatusCode(StatusCodes.Status201Created);
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product product) {
            if (ModelState.IsValid) {
                return TryUpdate(id, product);
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }

        private IActionResult TryUpdate(int id, Product product) {
            try {
                product.Id = id;
                productsDbContext.Products.Update(product);
                productsDbContext.SaveChanges(true);
                return StatusCode(StatusCodes.Status200OK);
            } catch {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            Product product = productsDbContext.Products.SingleOrDefault(m => m.Id == id);
            if (product == null) {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            productsDbContext.Products.Remove(product);
            productsDbContext.SaveChanges(true);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}