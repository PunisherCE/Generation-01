using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class HighScoreManager : MonoBehaviour
{
    private const string HighScoreKey = "HighScores"; //Key for PlayerPrefs

    public static void SaveHighScore(int newScore)
    {
        // Load existing scores
        HighScoreData highScores = LoadHighScores();

        // Add new score and sort in descending order
        highScores.scores.Add(newScore);
        highScores.scores = highScores.scores.OrderByDescending(s => s).Take(10).ToList(); // Keep top 10

        // Convert list to JSON and save in PlayerPrefs
        string json = JsonUtility.ToJson(highScores);
        PlayerPrefs.SetString(HighScoreKey, json);
        PlayerPrefs.Save();
    }

    public static HighScoreData LoadHighScores()
    {
        if (PlayerPrefs.HasKey(HighScoreKey))
        {
            string json = PlayerPrefs.GetString(HighScoreKey);
            return JsonUtility.FromJson<HighScoreData>(json);
        }
        return new HighScoreData(); // Return empty if no data exists
    }
}
