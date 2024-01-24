using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vizsgaremek.Dtos;
using Vizsgaremek.Services.Interfaces;

namespace Vizsgaremek.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var productDtos = await _productService.GetAllProductsAsync();
            return Ok(productDtos);
        }

        // GET: api/Product/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var productDto = await _productService.GetProductByIdAsync(id);

            if (productDto == null)
            {
                return NotFound();
            }

            return Ok(productDto);
        }

        // POST: api/Product
        [HttpPost]
        public async Task<ActionResult<int>> CreateProduct([FromBody] ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                var newProductId = await _productService.CreateProductAsync(productDto);
                return CreatedAtAction(nameof(GetProduct), new { id = newProductId }, newProductId);
            }
            else
            {
                var message = string.Join(" | ", ModelState.Values
                                    .SelectMany(v => v.Errors)
                                    .Select(e => e.ErrorMessage));
                return BadRequest(message);
            }
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.UpdateProductAsync(id, productDto);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            else
            {
                var message = string.Join(" | ", ModelState.Values
                                    .SelectMany(v => v.Errors)
                                    .Select(e => e.ErrorMessage));
                return BadRequest(message);
            }
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProductAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        // GET: api/Product/{id}/ProductShelves
        [HttpGet("{id}/ProductShelves")]
        public async Task<ActionResult<IEnumerable<ProductShelfDto>>> GetProductShelves(int id)
        {
            var productShelfDtos = await _productService.GetProductShelvesAsync(id);
            return Ok(productShelfDtos);
        }

        // GET: api/Product/Search?searchString=abc 
        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> SearchProducts([FromQuery] string searchString)
        {
            var productDtos = await _productService.SearchProductsAsync(searchString);
            return Ok(productDtos);
        }
    }
}
