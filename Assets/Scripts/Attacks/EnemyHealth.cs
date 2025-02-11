using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public EnemySpawner spawner;
    public CharacterSkeleton player;

    public int maxHealth = 50;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterSkeleton>();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Enemy took damage! Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy defeated!");
        Destroy(gameObject); // Or play a death animation
        spawner.enemiesKilled++;
        if(spawner.enemiesKilled >= spawner.numberOfEnemies)
        {
            player.health = 100;
            spawner.numberOfEnemies++;
            spawner.enemiesKilled = 0;
            spawner.SpawnEnemies(spawner.numberOfEnemies);
        }
    }
}
