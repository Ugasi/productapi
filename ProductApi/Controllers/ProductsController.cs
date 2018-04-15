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
        public IEnumerable<Product> Get() {
            return productsDbContext.Products;
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
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpDelete]
        public IActionResult Delete(int id) {
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}