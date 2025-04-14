using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnvironmentAPI.Models
{
    public class World
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        // 🔥 Belangrijk: deze was nodig voor de foutmelding
        public int UserId { get; set; }

        // Navigatieproperty naar de gebruiker
        public User User { get; set; }

        // Lijst van objecten in de wereld
        public List<WorldObject> WorldObjects { get; set; } = new();
    }
}
