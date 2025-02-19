using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackButtons : MonoBehaviour
{

    [SerializeField] GameObject panelMain;
    [SerializeField] GameObject panelOther;

    public void GoBack()
    {
        panelOther.SetActive(false);
        panelMain.SetActive(true);
    }



}
