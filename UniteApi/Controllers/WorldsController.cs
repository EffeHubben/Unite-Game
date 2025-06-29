using Microsoft.AspNetCore.Mvc;
using UniteApi.Models;
using UniteApi.Data;
using Microsoft.EntityFrameworkCore;

namespace UniteApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorldsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WorldsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("save")]
        public async Task<IActionResult> SaveWorld([FromBody] WorldEntity world)
        {
            if (world == null || world.WorldObjects == null)
                return BadRequest("Ongeldige data");

            _context.Worlds.Add(world);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Wereld opgeslagen!", worldId = world.Id });
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetWorldsByUser(int userId)
        {
            var worlds = _context.Worlds
                .Include(w => w.WorldObjects)
                .Where(w => w.UserId == userId)
                .ToList();

            return Ok(worlds);
        }
    }
}
