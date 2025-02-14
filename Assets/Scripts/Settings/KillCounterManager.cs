using UnityEngine;
using TMPro; // Required for TextMeshPro UI

public class KillCounterManager : MonoBehaviour
{
    public static KillCounterManager Instance; // Singleton reference
    public int enemiesKilled = 0;
    public TextMeshProUGUI killCounterText; // Assign in Inspector

    void Awake()
    {
        // Ensure only one instance of KillCounterManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keeps it alive between scene reloads
        }
        else
        {
            Destroy(gameObject); // Prevents duplicates
        }
    }

    public void UpdateKillCount()
    {
        enemiesKilled++;
        if (killCounterText != null)
        {
            killCounterText.text = "Kills: " + enemiesKilled;
        }
    }
}

