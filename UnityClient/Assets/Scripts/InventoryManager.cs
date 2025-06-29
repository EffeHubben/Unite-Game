using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public GameObject[] itemPrefabs;
    public Transform worldParent;
    public GameObject selectedPrefab;
    public TMP_InputField worldNameInput;
    public APIManager apiManager;
    public CanvasGroup inventoryPanelGroup;

    private GameObject previewObject;

    void Update()
    {
        if (previewObject != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            previewObject.transform.position = mousePosition;

            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                previewObject.transform.SetParent(worldParent);
                previewObject = null;

                // Toon het menu weer
                inventoryPanelGroup.alpha = 1;
                inventoryPanelGroup.interactable = true;
                inventoryPanelGroup.blocksRaycasts = true;

                Debug.Log("Object geplaatst!");
            }
        }
    }

    public void SelectObject(int index)
    {
        Debug.Log($"SelectObject aangeroepen met index: {index}");

        if (index >= 0 && index < itemPrefabs.Length)
        {
            selectedPrefab = itemPrefabs[index];
            previewObject = Instantiate(selectedPrefab);
            previewObject.transform.SetParent(null); // geen parent nog

            // Verberg UI tijdens plaatsing
            inventoryPanelGroup.alpha = 0;
            inventoryPanelGroup.interactable = false;
            inventoryPanelGroup.blocksRaycasts = false;

            Debug.Log($"Geselecteerd object: {selectedPrefab.name}");
        }
        else
        {
            Debug.LogWarning("Ongeldige index bij object selectie.");
        }
    }

    public void DeleteAllObjects()
    {
        foreach (Transform child in worldParent)
        {
            Destroy(child.gameObject);
        }
        Debug.Log("Alle objecten verwijderd!");
    }

    public void SaveWorld()
    {
        string worldName = worldNameInput.text;
        if (string.IsNullOrWhiteSpace(worldName))
        {
            Debug.LogWarning("Geef de wereld een naam!");
            return;
        }

        int userId = PlayerPrefs.GetInt("UserId", 1); // test-gebruiker

        GameObject[] sceneObjects = new GameObject[worldParent.childCount];
        for (int i = 0; i < worldParent.childCount; i++)
        {
            sceneObjects[i] = worldParent.GetChild(i).gameObject;
        }

        apiManager.SaveWorld(worldName, userId, sceneObjects);
        Debug.Log($"Wereld '{worldName}' opgeslagen met {sceneObjects.Length} object(en).");
    }
}
