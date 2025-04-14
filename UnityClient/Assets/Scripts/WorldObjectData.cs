using System;

[Serializable]
public class WorldObjectData
{
    // Voor APIManager
    public string prefabName;
    public Vector3Data position;
    public Vector3Data scale;
    public QuaternionData rotation;

    // Voor InventoryManager
    public string objectType;
    public float positionX;
    public float positionY;
    public float rotationZ;
    public float scaleX;
    public float scaleY;
}
