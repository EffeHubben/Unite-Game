using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using UnityEngine.Networking;

public class SlotManager : MonoBehaviour
{
    [Header("UI Buttons")]
    public Button[] slotButtons;         // Sleep hier je 5 buttons heen
    public TMP_Text[] slotButtonLabels;  // Sleep hier de bijbehorende TextMeshPro-tekstcomponenten

    [Header("Kleuren")]
    public Color newWorldColor = Color.gray;
    public Color existingWorldColor = Color.white;

    // ✅ Gebruik HTTP voor lokale verbinding zonder SSL-fout
    private string apiBaseUrl = "http://127.0.0.1:7077/api/world";

    void Start()
    {
        // ⚠️ Check op consistentie tussen knoppen en labels
        if (slotButtons.Length != slotButtonLabels.Length)
        {
            Debug.LogError("Aantal slotButtons en slotButtonLabels komt niet overeen!");
            return;
        }

        int userId = PlayerPrefs.GetInt("UserId", 1);

        for (int i = 0; i < slotButtons.Length; i++)
        {
            int slot = i + 1;
            StartCoroutine(SetupSlotButton(userId, slot, slotButtonLabels[i], slotButtons[i]));
            slotButtons[i].onClick.AddListener(() => OnSlotClicked(slot));
        }
    }

    IEnumerator SetupSlotButton(int userId, int slot, TMP_Text label, Button button)
    {
        using var www = UnityWebRequest.Get($"{apiBaseUrl}/user/{userId}/slot/{slot}");
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success && www.downloadHandler.text != "null")
        {
            var world = JsonUtility.FromJson<WorldData>(www.downloadHandler.text);
            label.text = world.name;
            button.GetComponent<Image>().color = existingWorldColor;
        }
        else
        {
            Debug.LogWarning($"Slot {slot} bevat geen bestaande wereld of gaf fout: {www.error}");
            label.text = "New World";
            button.GetComponent<Image>().color = newWorldColor;
        }
    }

    void OnSlotClicked(int slot)
    {
        PlayerPrefs.SetInt("SelectedSlot", slot);
        StartCoroutine(LoadOrCreateWorld(slot));
    }

    IEnumerator LoadOrCreateWorld(int slot)
    {
        int userId = PlayerPrefs.GetInt("UserId", 1);
        using var www = UnityWebRequest.Get($"{apiBaseUrl}/user/{userId}/slot/{slot}");
        yield return www.SendWebRequest();

        string json;
        if (www.result == UnityWebRequest.Result.Success && www.downloadHandler.text != "null")
        {
            json = www.downloadHandler.text;
        }
        else
        {
            Debug.Log($"Slot {slot} is leeg, maak nieuwe wereld aan.");
            var empty = new WorldData
            {
                name = $"World_{slot}",
                userId = userId,
                slot = slot,
                worldObjects = new System.Collections.Generic.List<WorldObjectData>()
            };
            json = JsonUtility.ToJson(empty);
        }

        PlayerPrefs.SetString("LoadedWorldJson", json);
        SceneManager.LoadScene("game");
    }

    // ✅ Data structs
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
        public System.Collections.Generic.List<WorldObjectData> worldObjects;
    }
}
