using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;   // Nodig voor SceneManager.LoadScene
using System.Collections;
using TMPro;

public class AuthManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_InputField usernameInput;   // sleep hier je gebruikersnaamveld naartoe
    public TMP_InputField passwordInput;   // sleep hier je wachtwoordveld naartoe
    public TMP_Text feedbackText;          // sleep hier je TMP Text naartoe voor meldingen

    // Base-URL van je API (pas poort aan indien nodig)
    private string apiBaseUrl = "http://127.0.0.1:7077/api/auth";

    // Wordt aangeroepen door je Register-knop
    public void OnRegisterButton()
    {
        string user = usernameInput.text.Trim();
        string pass = passwordInput.text.Trim();

        if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
        {
            feedbackText.text = "Vul gebruikersnaam & wachtwoord in!";
            return;
        }

        StartCoroutine(RegisterUser(user, pass));
    }

    // Wordt aangeroepen door je Login-knop
    public void OnLoginButton()
    {
        string user = usernameInput.text.Trim();
        string pass = passwordInput.text.Trim();

        if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
        {
            feedbackText.text = "Vul gebruikersnaam & wachtwoord in!";
            return;
        }

        StartCoroutine(LoginUser(user, pass));
    }

    private IEnumerator RegisterUser(string username, string password)
    {
        feedbackText.text = "Registreren…";

        // Serialiseer naar JSON
        var dto = new UserData { username = username, passwordHash = password };
        string json = JsonUtility.ToJson(dto);

        // Maak een POST request met raw JSON
        using UnityWebRequest www = new UnityWebRequest($"{apiBaseUrl}/register", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
            feedbackText.text = "Registratie geslaagd!";
        else
            feedbackText.text = "Fout bij registratie: " + www.error;
    }

    private IEnumerator LoginUser(string username, string password)
    {
        feedbackText.text = "Inloggen…";

        var dto = new UserData { username = username, passwordHash = password };
        string json = JsonUtility.ToJson(dto);

        using UnityWebRequest www = new UnityWebRequest($"{apiBaseUrl}/login", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            feedbackText.text = "Login geslaagd!";
            // Laad daarna de slot-selectie scene
            SceneManager.LoadScene("Load game");
        }
        else
        {
            feedbackText.text = "Fout bij login: " + www.error;
        }
    }

    [System.Serializable]
    public class UserData
    {
        public string username;
        public string passwordHash;
    }
}
