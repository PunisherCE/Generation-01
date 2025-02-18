using UnityEngine;
using UnityEngine.UI;

public class Diffuculty : MonoBehaviour
{
    public EnemySpawner spawner;
    public GameObject difficultyObj;
    public short difficulty;

    private Button button;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetDifficulty);
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0f;
    }


    void SetDifficulty()
    {
        EnemyAttack.difficulty = difficulty;
        EnemyHealth.difficulty = difficulty;

        Time.timeScale = 1f; // Unpause the game
        Cursor.lockState = CursorLockMode.Locked;
        spawner.SpawnEnemies(spawner.numberOfEnemies);
        difficultyObj.SetActive(false);
    }
}
