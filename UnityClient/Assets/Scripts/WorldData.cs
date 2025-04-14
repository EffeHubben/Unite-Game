using System;
using System.Collections.Generic;

[Serializable]
public class WorldData
{
    // Voor APIManager
    public string name;
    public int userId;
    public List<WorldObjectData> worldObjects = new();

    // Voor InventoryManager
    public string worldName;
    public int accountId;
    public List<WorldObjectData> objects = new();
}
