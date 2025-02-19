using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Button play;
    [SerializeField] Button highScores;
    [SerializeField] Button credits;
    [SerializeField] GameObject panelMain;
    [SerializeField] GameObject panelHS;
    [SerializeField] GameObject panelCredits;

    public void PlayGame()
    {
        SceneManager.LoadScene("Arena");
    }

    public void HighScores()
    {
        panelMain.SetActive(false);
        panelHS.SetActive(true);
    }

    public void Credits()
    {
        panelMain.SetActive(false);
        panelCredits.SetActive(true);
    }
}