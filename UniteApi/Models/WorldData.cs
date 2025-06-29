using System.Collections.Generic;

namespace UniteApi.Models
{
    public class WorldData
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        public int Slot { get; set; }  // Toegevoegd: opslaan in slot 1-5

        public List<WorldObjectData> WorldObjects { get; set; } = new();
    }
}
