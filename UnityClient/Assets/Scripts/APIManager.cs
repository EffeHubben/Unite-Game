using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

[System.Serializable]
public class Vector3Data
{
    public float x, y, z;
}

[System.Serializable]
public class QuaternionData
{
    public float x, y, z, w;
}

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
    public List<WorldObjectData> worldObjects = new();
}

public class APIManager : MonoBehaviour
{
    public string apiBaseUrl = "https://localhost:7234/api/world"; // Pas dit aan naar jouw poort

    public void SaveWorld(string worldName, int userId, GameObject[] sceneObjects)
    {
        WorldData world = new WorldData();
        world.name = worldName;
        world.userId = userId;

        foreach (var obj in sceneObjects)
        {
            WorldObjectData wObj = new WorldObjectData
            {
                prefabName = obj.name.Replace("(Clone)", "").Trim(),
                position = new Vector3Data
                {
                    x = obj.transform.position.x,
                    y = obj.transform.position.y,
                    z = obj.transform.position.z
                },
                scale = new Vector3Data
                {
                    x = obj.transform.localScale.x,
                    y = obj.transform.localScale.y,
                    z = obj.transform.localScale.z
                },
                rotation = new QuaternionData
                {
                    x = obj.transform.rotation.x,
                    y = obj.transform.rotation.y,
                    z = obj.transform.rotation.z,
                    w = obj.transform.rotation.w
                }
            };
            world.worldObjects.Add(wObj);
        }

        string json = JsonUtility.ToJson(world);
        StartCoroutine(PostWorld(json));
    }

    IEnumerator PostWorld(string json)
    {
        UnityWebRequest www = new UnityWebRequest(apiBaseUrl + "/save", "POST");
        byte[] body = Encoding.UTF8.GetBytes(json);
        www.uploadHandler = new UploadHandlerRaw(body);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
            Debug.Log("✅ Wereld succesvol opgeslagen!");
        else
            Debug.LogError("❌ Fout bij opslaan: " + www.error);
    }

    public void LoadWorldsByUser(int userId)
    {
        StartCoroutine(GetWorldsRoutine(userId));
    }

    IEnumerator GetWorldsRoutine(int userId)
    {
        UnityWebRequest www = UnityWebRequest.Get($"{apiBaseUrl}/user/{userId}");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string json = "{\"items\":" + www.downloadHandler.text + "}";

            WorldListWrapper wrapper = JsonUtility.FromJson<WorldListWrapper>(json);
            Debug.Log("✅ Werelden ontvangen: " + wrapper.items.Count);

            foreach (var world in wrapper.items)
            {
                Debug.Log($"🌍 {world.name} heeft {world.worldObjects.Count} objecten.");
                // Hier kan je buttons maken of werelden instantiëren
            }
        }
        else
        {
            Debug.LogError("❌ Fout bij ophalen: " + www.error);
        }
    }
}

[System.Serializable]
public class WorldListWrapper
{
    public List<WorldData> items;
}
