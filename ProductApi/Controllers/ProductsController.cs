using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;

namespace ProductApi.Controllers {
    [Route("api/Products")]
    public class ProductsController : Controller {
        static List<Product> products = new List<Product>() {
            new Product(){Id = 1, name = "First product", price = 10, category = "Some category", manufacturer = "Acer", productCode = "1234567890"},
            new Product(){Id = 1, name = "Second product", price = 10.30, category = "Other category", manufacturer = "Asus", productCode = "0987654321"}
        };

        [HttpGet]
        public IActionResult Get() {
            return StatusCode(StatusCodes.Status200OK, products);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Product product) {
            if (ModelState.IsValid) {
                products.Add(product);
                return StatusCode(StatusCodes.Status201Created);
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Product product) {
            products[id] = product;
        }

        [HttpDelete]
        public void Delete(int id) {
            products.RemoveAt(id);
        }
    }
}