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

    void Update()
    {
        if (selectedPrefab != null && Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            Instantiate(selectedPrefab, mousePosition, Quaternion.identity, worldParent);
        }
    }

    public void SelectObject(int index)
    {
        if (index >= 0 && index < itemPrefabs.Length)
        {
            selectedPrefab = itemPrefabs[index];
        }
        else
        {
            Debug.LogWarning("❌ Ongeldige index bij object selectie.");
        }
    }

    public void DeleteAllObjects()
    {
        foreach (Transform child in worldParent)
        {
            Destroy(child.gameObject);
        }
        Debug.Log("🗑️ Alle objecten verwijderd!");
    }

    public void SaveWorld()
    {
        string worldName = worldNameInput.text;
        if (string.IsNullOrWhiteSpace(worldName))
        {
            Debug.LogWarning("⚠️ Geef de wereld een naam!");
            return;
        }

        int userId = PlayerPrefs.GetInt("UserId", 1);

        GameObject[] sceneObjects = new GameObject[worldParent.childCount];
        for (int i = 0; i < worldParent.childCount; i++)
        {
            sceneObjects[i] = worldParent.GetChild(i).gameObject;
        }

        apiManager.SaveWorld(worldName, userId, sceneObjects);
    }
}
