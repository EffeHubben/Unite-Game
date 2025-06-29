using UnityEngine;
using System.Collections.Generic;

public class WorldLoader : MonoBehaviour
{
    public Transform worldParent;
    public GameObject[] prefabTemplates; // Zorg dat prefabTemplates[0] = Lamp, [1] = Table, etc.

    void Start()
    {
        // 1) Lees de JSON uit PlayerPrefs
        string json = PlayerPrefs.GetString("LoadedWorldJson", "");
        if (string.IsNullOrEmpty(json))
        {
            Debug.LogError("Geen wereld-data gevonden in PlayerPrefs!");
            return;
        }

        // 2) Parse naar een helper-klasse
        WorldData world = JsonUtility.FromJson<WorldData>(json);

        // 3) Instantiate elk object
        foreach (var obj in world.worldObjects)
        {
            // Zoek de juiste prefab op basis van prefabName
            for (int i = 0; i < prefabTemplates.Length; i++)
            {
                if (prefabTemplates[i].name == obj.prefabName)
                {
                    Vector3 pos = new Vector3(obj.position.x, obj.position.y, obj.position.z);
                    GameObject go = Instantiate(prefabTemplates[i], pos,
                                     Quaternion.Euler(obj.rotation.x, obj.rotation.y, obj.rotation.z),
                                     worldParent);
                    go.transform.localScale = new Vector3(
                        obj.scale.x, obj.scale.y, obj.scale.z
                    );
                    break;
                }
            }
        }

        Debug.Log($"🌍 Wereld '{world.name}' geladen met {world.worldObjects.Count} objecten.");
    }

    // Zelfde helper classes als in APIManager:
    [System.Serializable]
    public class Vector3Data { public float x, y, z; }
    [System.Serializable]
    public class QuaternionData { public float x, y, z, w; }
    [System.Serializable]
    public class WorldObjectData
    {
        public string prefabName;
        public Vector3Data position;
        public Vector3Data scale;
        public QuaternionData rotation;
    }
    [System.Serializable]
    public class WorldData
    {
        public string name;
        public int userId;
        public int slot;
        public List<WorldObjectData> worldObjects;
    }
}
