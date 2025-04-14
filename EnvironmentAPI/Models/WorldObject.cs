using System.ComponentModel.DataAnnotations;

namespace EnvironmentAPI.Models
{
    public class WorldObject
    {
        public int Id { get; set; }

        [Required]
        public string PrefabName { get; set; }

        public Vector3Data Position { get; set; }
        public Vector3Data Scale { get; set; }
        public QuaternionData Rotation { get; set; }

        public World? World { get; set; }
    }
}
