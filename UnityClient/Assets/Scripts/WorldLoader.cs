using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class WorldLoader : MonoBehaviour
{
    public string apiBaseUrl = "https://localhost:7234/api/world";

    void Start()
    {
        int userId = PlayerPrefs.GetInt("UserId");
        StartCoroutine(LoadWorlds(userId));
    }

    IEnumerator LoadWorlds(int userId)
    {
        UnityWebRequest www = UnityWebRequest.Get($"{apiBaseUrl}/user/{userId}");
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string json = "{\"items\":" + www.downloadHandler.text + "}";

            WorldListWrapper wrapper = JsonUtility.FromJson<WorldListWrapper>(json);
            Debug.Log("✅ Werelden geladen: " + wrapper.items.Count);

            foreach (var world in wrapper.items)
            {
                Debug.Log($"🌍 {world.name} met {world.worldObjects.Count} objecten");
                // Hier kun je knoppen maken of automatisch 1 wereld laden
            }
        }
        else
        {
            Debug.LogError("❌ Fout bij ophalen werelden: " + www.error);
        }
    }
}
