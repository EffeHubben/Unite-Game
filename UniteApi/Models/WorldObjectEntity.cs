using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace UniteApi.Models
{
    public class WorldObjectEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string PrefabName { get; set; }

        [Required]
        public Vector3Data Position { get; set; }

        [Required]
        public Vector3Data Scale { get; set; }

        [Required]
        public QuaternionData Rotation { get; set; }

        // Relatie naar de wereld
        public int WorldEntityId { get; set; }

        [ForeignKey("WorldEntityId")]
        public WorldEntity World { get; set; }
    }
}
