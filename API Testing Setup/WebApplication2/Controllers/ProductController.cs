using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Model;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private static List<Product> _products = new()
        {
            new Product { Id = 1, Name = "Laptop", Price = 999.99M },
        new Product { Id = 2, Name = "Smartphone", Price = 499.99M }
        };

        [HttpGet(Name = "GetAllProducts")]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            return Ok(_products);
        }

        [HttpGet("{id}", Name = "GetProductById")]
        public ActionResult<Product> GetById(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost(Name = "CreateProduct")]
        public ActionResult Create(Product product)
        {
            product.Id = _products.Count + 1;
            _products.Add(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }


        [HttpGet("expensive", Name = "GetExpensiveProducts")]
        public ActionResult<IEnumerable<Product>> GetExpensiveProduct()
        {
            var expensiveProducts = _products.Where(p => p.Price > 500).ToList();
            if (!expensiveProducts.Any())
            {
                return NotFound("No expensive products found");
            }
            return Ok(expensiveProducts);
        }

        [HttpPut("{id}", Name = "UpdateProduct")]
        public ActionResult Update(int id, Product product)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteProduct")]
        public ActionResult Delete(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            _products.Remove(product);
            return NoContent();
        }

        [HttpGet("cheap", Name = "GetCheapProducts")]
        public ActionResult<IEnumerable<Product>> GetCheapProducts()
        {
            var cheapProducts = _products.Where(p => p.Price <= 500).ToList();
            return Ok(cheapProducts);
        }
    }
}
