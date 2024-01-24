using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vizsgaremek.Dtos;
using Vizsgaremek.Services.Interfaces;

namespace Vizsgaremek.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShelfProductController : ControllerBase
    {
        private readonly IShelfProductService _shelfProductService;

        public ShelfProductController(IShelfProductService shelfProductService)
        {
            _shelfProductService = shelfProductService;
        }

        // GET: api/ShelfProduct
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShelfProductDto>>> GetShelfProducts()
        {
            var shelfProductDtos = await _shelfProductService.GetAllShelfProductsAsync();
            return Ok(shelfProductDtos);
        }

        // GET: api/ShelfProduct/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ShelfProductDto>> GetShelfProduct(int id)
        {
            var shelfProductDto = await _shelfProductService.GetShelfProductByIdAsync(id);

            if (shelfProductDto == null)
            {
                return NotFound();
            }

            return Ok(shelfProductDto);
        }

        // POST: api/ShelfProduct
        [HttpPost]
        public async Task<ActionResult<int>> CreateShelfProduct([FromBody] ShelfProductDto shelfProductDto)
        {
            if (ModelState.IsValid)
            {
                var newShelfProductId = await _shelfProductService.CreateShelfProductAsync(shelfProductDto);
                return CreatedAtAction(nameof(GetShelfProduct), new { id = newShelfProductId }, newShelfProductId);
            }
            else
            {
                var message = string.Join(" | ", ModelState.Values
                                                       .SelectMany(v => v.Errors)
                                                                                          .Select(e => e.ErrorMessage));
                return BadRequest(message);
            }
        }

        // PUT: api/ShelfProduct/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateShelfProduct(int id, [FromBody] ShelfProductDto shelfProductDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _shelfProductService.UpdateShelfProductAsync(id, shelfProductDto);
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

        // DELETE: api/ShelfProduct/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteShelfProduct(int id)
        {
            var shelfProductDto = await _shelfProductService.GetShelfProductByIdAsync(id);

            if (shelfProductDto == null)
            {
                return NotFound();
            }

            await _shelfProductService.DeleteShelfProductAsync(id);

            return NoContent();
        }
    }
}
