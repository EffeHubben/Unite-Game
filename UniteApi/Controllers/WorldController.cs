using Microsoft.AspNetCore.Mvc;
using UniteApi.Models;

namespace UniteApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorldController : ControllerBase
    {
        private static List<WorldData> _worlds = new(); // Tijdelijke opslag (in-memory)

        [HttpPost("save")]
        public IActionResult SaveWorld([FromBody] WorldData world)
        {
            if (world == null || string.IsNullOrEmpty(world.Name))
                return BadRequest("Ongeldige werelddata.");

            _worlds.Add(world);
            Console.WriteLine($"Wereld '{world.Name}' opgeslagen met {world.WorldObjects.Count} object(en).");

            return Ok(new { message = "Wereld succesvol opgeslagen!" });
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetWorldsByUser(int userId)
        {
            var userWorlds = _worlds.Where(w => w.UserId == userId).ToList();
            return Ok(userWorlds);
        }
    }
}
