using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] AudioSource soundSource;
    [SerializeField] AudioClip clipHit;
    [SerializeField] AudioClip clipDie;

    public EnemySpawner spawner;
    public CharacterSkeleton player;
    public int maxHealth = 50;

    private EnemyAttack attack;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        attack = GetComponent<EnemyAttack>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterSkeleton>();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Enemy took damage! Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            attack.enabled = false;
            StartCoroutine(Die());
        } else soundSource.PlayOneShot(clipHit);
    }

    private IEnumerator Die()
    {
        soundSource.PlayOneShot(clipDie);
        Debug.Log("Enemy defeated!");
        yield return new WaitForSeconds(0.5f);
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
