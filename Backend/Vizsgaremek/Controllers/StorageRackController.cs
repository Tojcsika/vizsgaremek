using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vizsgaremek.Dtos;
using Vizsgaremek.Services.Interfaces;

namespace Vizsgaremek.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StorageRackController : ControllerBase
    {
        private readonly IStorageRackService _storageRackService;

        public StorageRackController(IStorageRackService storageRackService)
        {
            _storageRackService = storageRackService;
        }

        // GET: api/StorageRack
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StorageRackDto>>> GetStorageRacks()
        {
            var storageRackDtos = await _storageRackService.GetAllStorageRacksAsync();
            return Ok(storageRackDtos);
        }

        // GET: api/StorageRack/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<StorageRackDto>> GetStorageRack(int id)
        {
            var storageRackDto = await _storageRackService.GetStorageRackByIdAsync(id);

            if (storageRackDto == null)
            {
                return NotFound();
            }

            return Ok(storageRackDto);
        }

        // POST: api/StorageRack
        [HttpPost]
        public async Task<ActionResult<int>> CreateStorageRack([FromBody] StorageRackDto storageRackDto)
        {
            if (ModelState.IsValid)
            {
                var newStorageRackId = await _storageRackService.CreateStorageRackAsync(storageRackDto);
                return CreatedAtAction(nameof(GetStorageRack), new { id = newStorageRackId }, newStorageRackId);
            }
            else
            {
                var message = string.Join(" | ", ModelState.Values
                                    .SelectMany(v => v.Errors)
                                    .Select(e => e.ErrorMessage));
                return BadRequest(message);
            }
        }

        // PUT: api/StorageRack/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStorageRack(int id, [FromBody] StorageRackDto storageRackDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _storageRackService.UpdateStorageRackAsync(id, storageRackDto);
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

        // DELETE: api/StorageRack/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStorageRack(int id)
        {
            var result = await _storageRackService.DeleteStorageRackAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        // GET: api/StorageRack/{id}/Shelves
        [HttpGet("{id}/Shelves")]
        public async Task<ActionResult<StorageDto>> GetStorageRackShelves(int id)
        {
            var shelves = await _storageRackService.GetShelvesAsync(id);
            return Ok(shelves);
        }
    }

}
