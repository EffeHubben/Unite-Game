namespace UniteApi.Models
{
    public class WorldObjectData
    {
        public string PrefabName { get; set; }
        public Vector3Data Position { get; set; }
        public Vector3Data Scale { get; set; }
        public QuaternionData Rotation { get; set; }
    }
}
