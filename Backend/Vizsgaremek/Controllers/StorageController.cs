using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vizsgaremek.Dtos;
using Vizsgaremek.Services.Interfaces;

namespace Vizsgaremek.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StorageController : ControllerBase
    {
        private readonly IStorageService _storageService;

        public StorageController(IStorageService storageService)
        {
            _storageService = storageService;
        }

        // GET: api/Storage
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StorageDto>>> GetStorages()
        {
            var storageDtos = await _storageService.GetAllStoragesAsync();
            return Ok(storageDtos);
        }

        // GET: api/Storage/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<StorageDto>> GetStorage(int id)
        {
            var storageDto = await _storageService.GetStorageByIdAsync(id);

            if (storageDto == null)
            {
                return NotFound();
            }

            return Ok(storageDto);
        }

        // POST: api/Storage
        [HttpPost]
        public async Task<ActionResult<int>> CreateStorage([FromBody] StorageDto storageDto)
        {
            if (ModelState.IsValid)
            {
                var newStorageId = await _storageService.CreateStorageAsync(storageDto);
                return CreatedAtAction(nameof(GetStorage), new { id = newStorageId }, newStorageId);
            }
            else
            {
                var message = string.Join(" | ", ModelState.Values
                                    .SelectMany(v => v.Errors)
                                    .Select(e => e.ErrorMessage));
                return BadRequest(message);
            }
        }

        // PUT: api/Storage/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStorage(int id, [FromBody] StorageDto storageDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _storageService.UpdateStorageAsync(id, storageDto);
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

        // DELETE: api/Storage/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStorage(int id)
        {
            var result = await _storageService.DeleteStorageAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        // GET: api/Storage/{id}/StorageRacks
        [HttpGet("{id}/StorageRacks")]
        public async Task<ActionResult<StorageDto>> GetStorageStorageRacks(int id)
        {
            var storageRacks = await _storageService.GetStorageRacksAsync(id);
            return Ok(storageRacks);
        }
    }
}
