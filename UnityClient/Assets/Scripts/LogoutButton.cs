using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoutButton : MonoBehaviour
{
    public void OnLogoutClicked()
    {
        // Optioneel: verwijder opgeslagen user info
        PlayerPrefs.DeleteKey("UserId");
        PlayerPrefs.DeleteKey("SelectedSlot");

        // Ga terug naar login scene
        SceneManager.LoadScene("Login and Register");
    }
}
