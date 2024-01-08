using Vizsgaremek.Dtos.Brick;
using Vizsgaremek.Repositories;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace Vizsgaremek.Controllers
{
    [Route("api/brick")]
    [ApiController]
    public class BrickController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBrickRepository _brickRepository;

        public BrickController(IMapper mapper, IBrickRepository brickRepository)
        {
            _mapper = mapper;
            _brickRepository = brickRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var bricks = _brickRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<BrickTypeDto>>(bricks));
        }
    }
}
