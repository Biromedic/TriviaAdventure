using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Button musicButton; // Assign this in the Inspector

    private bool isSettingsOpen = false;

    public void ToggleSettings()
    {
        isSettingsOpen = !isSettingsOpen;
        musicButton.gameObject.SetActive(isSettingsOpen);
    }
}

