using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vizsgaremek.Dtos;
using Vizsgaremek.Services.Interfaces;

namespace Vizsgaremek.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShelfController : ControllerBase
    {
        private readonly IShelfService _shelfService;

        public ShelfController(IShelfService shelfService)
        {
            _shelfService = shelfService;
        }

        // GET: api/Shelf
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShelfDto>>> GetShelves()
        {
            var shelfDtos = await _shelfService.GetAllShelvesAsync();
            return Ok(shelfDtos);
        }

        // GET: api/Shelf/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ShelfDto>> GetShelf(int id)
        {
            var shelfDto = await _shelfService.GetShelfByIdAsync(id);

            if (shelfDto == null)
            {
                return NotFound();
            }

            return Ok(shelfDto);
        }

        // POST: api/Shelf
        [HttpPost]
        public async Task<ActionResult<int>> CreateShelf([FromBody] ShelfDto shelfDto)
        {
            if (ModelState.IsValid)
            {
                var newShelfId = await _shelfService.CreateShelfAsync(shelfDto);
                return CreatedAtAction(nameof(GetShelf), new { id = newShelfId }, newShelfId);
            }
            else
            {
                var message = string.Join(" | ", ModelState.Values
                                    .SelectMany(v => v.Errors)
                                    .Select(e => e.ErrorMessage));
                return BadRequest(message);
            }
        }

        // PUT: api/Shelf/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShelf(int id, [FromBody] ShelfDto shelfDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _shelfService.UpdateShelfAsync(id, shelfDto);
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

        // DELETE: api/Shelf/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShelf(int id)
        {
            var result = await _shelfService.DeleteShelfAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}