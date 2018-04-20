using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Data;
using ProductApi.Models;
using ProductApi.Services;

namespace ProductApi.Controllers {
    [ApiVersion("1.0")]
    [Route("api/products")]
    public class ProductsController : Controller {
        private const int DefaultPageNumber = 1;
        private const int DefaultPageSize = 10;
        IProduct productRepository;

        public ProductsController(IProduct productRepository) {
            this.productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult Get(string sortPrice, int? pageNumber, int? pageSize, string search) {
            int currentPage = pageNumber ?? DefaultPageNumber;
            int currentPageSize = pageSize ?? DefaultPageSize;
            string searchString = search ?? String.Empty;
            IQueryable<Product> products = GetProductsSorted(sortPrice, searchString);
            List<Product> pagedItems = products.Skip((currentPage - 1) * currentPageSize).Take(currentPageSize).ToList();
            return StatusCode(StatusCodes.Status200OK, pagedItems); ;
        }

        private IQueryable<Product> GetProductsSorted(string sortPrice, string search) {
            switch (sortPrice) {
                case "desc":
                    return productRepository.GetProducts()
                        .Where(p => p.Name.StartsWith(search, StringComparison.InvariantCultureIgnoreCase))
                        .OrderByDescending(p => p.Price);
                case "asc":
                    return productRepository.GetProducts()
                        .Where(p => p.Name.StartsWith(search, StringComparison.InvariantCultureIgnoreCase))
                        .OrderBy(p => p.Price);
                default:
                    return productRepository.GetProducts()
                        .Where(p => p.Name.StartsWith(search, StringComparison.InvariantCultureIgnoreCase));
            }
        }

        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id) {
            Product product = productRepository.GetProduct(id);
            if (product == null) {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return StatusCode(StatusCodes.Status200OK, product);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Product product) {
            if (ModelState.IsValid) {
                productRepository.AddProduct(product);
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
                productRepository.UpdateProduct(product);
                return StatusCode(StatusCodes.Status200OK);
            } catch {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            productRepository.DeleteProduct(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}