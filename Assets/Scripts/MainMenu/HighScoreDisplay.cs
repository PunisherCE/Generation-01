using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class HighScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI highScoreText; //Assign in Inspector

    void Start()
    {
        DisplayHighScores();
    }

    void DisplayHighScores()
    {
        HighScoreData highScores = HighScoreManager.LoadHighScores();
        highScoreText.text = "Top 10 High Scores:\n";

        for (int i = 0; i < highScores.scores.Count; i++)
        {
            highScoreText.text += (i + 1) + ". " + highScores.scores[i] + "\n";
        }
    }
}

