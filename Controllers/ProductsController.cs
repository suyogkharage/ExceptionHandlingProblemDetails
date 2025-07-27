using ExceptionHandlingProblemDetails.CustomExceptions;
using ExceptionHandlingProblemDetails.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExceptionHandlingProblemDetails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Product ID must be greater than 0.");
            }

            // Simulate a not-found scenario
            if (id != 1)
            {
                throw new NotFoundException($"Product with ID {id} was not found.");
            }

            return Ok(new Product { Id = 1, Name = "Laptop", Price = 999.99m });
        }

        [HttpPost]
        public IActionResult CreateProduct(Product? product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Product payload cannot be null.");
            }

            // Simulate exception
            if (product.Price < 0)
            {
                throw new InvalidOperationException("Price cannot be negative.");
            }

            return CreatedAtAction(nameof(GetProduct), new { id = 1 }, product);
        }
    }
}
