using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

[System.Serializable]
public class WorldListWrapper
{
    public List<WorldData> items;
}

public class APIManager : MonoBehaviour
{
    public string apiBaseUrl = "https://localhost:7234/api/world"; // Pas poort aan indien nodig

    public void SaveWorld(string worldName, int userId, GameObject[] sceneObjects)
    {
        WorldData world = new WorldData
        {
            name = worldName,
            userId = userId
        };

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

    private IEnumerator PostWorld(string json)
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

    private IEnumerator GetWorldsRoutine(int userId)
    {
        UnityWebRequest www = UnityWebRequest.Get($"{apiBaseUrl}/user/{userId}");
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string json = "{\"items\":" + www.downloadHandler.text + "}";
            WorldListWrapper wrapper = JsonUtility.FromJson<WorldListWrapper>(json);

            foreach (var world in wrapper.items)
            {
                Debug.Log($"🌍 {world.name} heeft {world.worldObjects.Count} objecten.");
            }
        }
        else
        {
            Debug.LogError("❌ Fout bij ophalen: " + www.error);
        }
    }
}
