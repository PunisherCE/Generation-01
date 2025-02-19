using UnityEngine;
using UnityEngine.UI;

public class MusicSettings : MonoBehaviour
{
    public Toggle muteToggle; //Assign in Inspector

    private const string MusicMuteKey = "MusicMute"; //Key to store mute state

    void Start()
    {
        // Load the mute state and set the toggle
        bool isMuted = PlayerPrefs.GetInt(MusicMuteKey, 0) == 1; // Default is unmuted
        muteToggle.isOn = isMuted;

        // Listen for toggle changes
        muteToggle.onValueChanged.AddListener(SetMute);
    }

    public void SetMute(bool isMuted)
    {
        PlayerPrefs.SetInt(MusicMuteKey, isMuted ? 1 : 0); // Save to PlayerPrefs
        PlayerPrefs.Save(); // Ensure data is saved
    }
}

