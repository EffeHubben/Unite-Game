using System.Collections.Generic;

namespace UniteApi.Models
{
    public class WorldEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Slot { get; set; }
        public int UserId { get; set; }

        // Navigatie-eigenschap (EF Core)
        public List<WorldObjectEntity> WorldObjects { get; set; }
    }
}
