using EnvironmentAPI.Data;
using EnvironmentAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnvironmentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorldController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WorldController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ 1. Wereld opslaan
        [HttpPost("save")]
        public IActionResult SaveWorld(World world)
        {
            foreach (var obj in world.WorldObjects)
            {
                obj.World = world;
            }

            _context.Worlds.Add(world);
            _context.SaveChanges();
            return Ok("Wereld opgeslagen");
        }

        // ✅ 2. Alle werelden ophalen voor 1 gebruiker
        [HttpGet("user/{userId}")]
        public IActionResult GetWorldsByUser(int userId)
        {
            var worlds = _context.Worlds
                .Where(w => w.UserId == userId)
                .Include(w => w.WorldObjects)
                .ToList();

            return Ok(worlds);
        }

        // ✅ 3. Eén wereld ophalen op ID
        [HttpGet("{id}")]
        public IActionResult GetWorldById(int id)
        {
            var world = _context.Worlds
                .Include(w => w.WorldObjects)
                .FirstOrDefault(w => w.Id == id);

            if (world == null)
                return NotFound("Wereld niet gevonden");

            return Ok(world);
        }

        // ✅ 4. Wereld verwijderen
        [HttpDelete("{id}")]
        public IActionResult DeleteWorld(int id)
        {
            var world = _context.Worlds
                .Include(w => w.WorldObjects)
                .FirstOrDefault(w => w.Id == id);

            if (world == null)
                return NotFound("Wereld niet gevonden");

            _context.WorldObjects.RemoveRange(world.WorldObjects);
            _context.Worlds.Remove(world);
            _context.SaveChanges();

            return Ok("Wereld verwijderd");
        }
    }
}
