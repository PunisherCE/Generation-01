using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    public static RoundManager Instance; // Singleton reference
    public static int gold = 5; //Starting amount of gold

    public EnemySpawner spawner;
    public CharacterSkeleton player;
    public GameObject uiContainer;

    public Button health;
    public Button damage;
    public Button startRound;
    public TextMeshProUGUI goldText; //UI Text for gold display

    private int healthUpgradeCost = 10;
    private int damageUpgradeCost = 20;

    void Awake()
    {
        // Ensure only one instance of RoundManager exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Prevents duplicates
        }
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterSkeleton>();

        UpdateUI(); //Initialize UI
    }

    public void SetHealth()
    {
        if (gold >= healthUpgradeCost)
        {
            gold -= healthUpgradeCost;
            player.maxHealth += 5;
            UpdateUI(); //Refresh UI after upgrade
        }
    }

    public void SetDamage()
    {
        if (gold >= damageUpgradeCost)
        {
            gold -= damageUpgradeCost;
            player.damage += 5;
            UpdateUI(); //Refresh UI after upgrade
        }
    }

    public void StartRound()
    {
        player.health = player.maxHealth;
        player.UpdateHealthUI();
        spawner.numberOfEnemies += 2;
        spawner.enemiesKilled = 0;
        spawner.SpawnEnemies(spawner.numberOfEnemies);

        //Disable upgrade buttons during combat
        //health.gameObject.SetActive(false);
        uiContainer.SetActive(false);
        health.interactable = false;
        damage.interactable = false;
        startRound.interactable = false;
        goldText.enabled = false;
        Time.timeScale = 1f; // Unpause the game
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void NextRound()
    {
        Cursor.lockState = CursorLockMode.Confined;
        AddGold(10);
        //health.gameObject.SetActive(true);
        uiContainer.SetActive(true);
        goldText.enabled = true;
        health.interactable = gold >= healthUpgradeCost;
        damage.interactable = gold >= damageUpgradeCost;
        startRound.interactable = true;
        Time.timeScale = 0f; // Pause the game
    }

    public void AddGold(int amount)
    {
        gold += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        goldText.text = "Gold: " + gold;
        health.interactable = gold >= healthUpgradeCost;
        damage.interactable = gold >= damageUpgradeCost;
    }
}
