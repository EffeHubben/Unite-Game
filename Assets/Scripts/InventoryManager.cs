using System.Collections;
using System.Collections.Generic;
using System.Net; // voor certificaat override (alleen tijdelijk!)
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [Header("Prefab Setup")]
    public GameObject[] objectPrefabs; // De objecten (bijv. Lamp, Chair)
    public Transform worldParent;      // Hier komen geplaatste objecten onder te hangen

    [Header("Preview Setup")]
    public Material previewMaterial;   // Transparant materiaal voor plaats-preview
    private GameObject selectedPrefab; // Het gekozen object
    private GameObject previewObject;  // Het spookobject dat je rondbeweegt

    [Header("UI")]
    public Canvas inventoryCanvas;
    public InputField worldNameInput; // Gebruiker vult wereldnaam in
    public Text statusText;           // Feedback naar speler
    
    [Header("Account / API")]
    public int currentAccountId = 1;
    public string apiUrl = "https://uniteapi-backend-dhapgjeahzhtf5c5.northeurope-01.azurewebsites.net/api/worlds/save";    // API naar je Azure-backend

    [Header("Object Data")]
    public List<WorldObjectData> placedObjects = new List<WorldObjectData>(); // Lijst met geplaatste objecten

    // 📌 Gebruiker klikt op "Lamp" of "Chair" → selecteert prefab
    public void SelectObject(int index)
    {
        selectedPrefab = objectPrefabs[index];

        if (previewObject != null)
            Destroy(previewObject);

        previewObject = Instantiate(selectedPrefab);
        previewObject.transform.localScale *= 0.9f;

        SpriteRenderer[] renderers = previewObject.GetComponentsInChildren<SpriteRenderer>();
        foreach (var r in renderers)
        {
            if (previewMaterial != null)
                r.material = previewMaterial;
        }

        inventoryCanvas.gameObject.SetActive(false); // UI even weg tijdens plaatsen
    }

    // 📌 Wordt continu uitgevoerd: preview volgt muis, object wordt geplaatst met klik
    void Update()
    {
        if (previewObject != null)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;
            previewObject.transform.position = mousePos;

            if (Input.GetMouseButtonDown(0)) // Linkermuisklik
            {
                GameObject newObj = Instantiate(selectedPrefab, mousePos, Quaternion.identity, worldParent);

                WorldObjectData data = new WorldObjectData
                {
                    objectType = selectedPrefab.name.Replace("(Clone)", "").Trim(),
                    positionX = mousePos.x,
                    positionY = mousePos.y,
                    rotationZ = newObj.transform.eulerAngles.z,
                    scaleX = newObj.transform.localScale.x,
                    scaleY = newObj.transform.localScale.y
                };

                placedObjects.Add(data);

                Destroy(previewObject);
                previewObject = null;
                selectedPrefab = null;

                inventoryCanvas.gameObject.SetActive(true); // UI weer terug
            }
        }
    }

    // 📌 Alles verwijderen
    public void ResetWorld()
    {
        foreach (Transform child in worldParent)
            Destroy(child.gameObject);

        placedObjects.Clear(); // Ook de data wissen
    }

    // 📌 Opslaan-knop wordt aangeklikt
    public void SaveWorld()
    {
        StartCoroutine(SaveWorldCoroutine());
    }

    // 📌 Coroutine die JSON maakt en POST verstuurt
    private IEnumerator SaveWorldCoroutine()
    {
        if (string.IsNullOrEmpty(worldNameInput.text))
        {
            statusText.text = "Please enter a world name.";
            yield break;
        }

        // Maak het WorldData-object aan met alle info
        WorldData world = new WorldData
        {
            worldName = worldNameInput.text,
            accountId = currentAccountId,
            objects = placedObjects
        };

        // Zet om naar JSON
        string json = JsonUtility.ToJson(world, true);


        // ✅ Controleer of je hier HTTPS gebruikt in je apiUrl!
        UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");
        byte[] body = Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(body);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // Logging om te controleren wat er wordt verzonden
        Debug.Log("URL: " + apiUrl);
        Debug.Log("Payload: " + json);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + request.error);
            statusText.text = "Save failed: " + request.error;
        }
        else
        {
            Debug.Log("Saved: " + request.downloadHandler.text);
            statusText.text = "World saved successfully!";
        }

        Debug.Log("Status code: " + request.responseCode);
        Debug.Log("Download text: " + request.downloadHandler.text);
        Debug.Log("Error (if any): " + request.error);
    }
}