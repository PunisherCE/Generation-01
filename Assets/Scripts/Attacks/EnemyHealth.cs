using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    static public short difficulty = 0;

    [SerializeField] AudioSource soundSource;
    [SerializeField] AudioClip clipHit;
    [SerializeField] AudioClip clipDie;

    public ParticleSystem particleEffect; // Assign this in Inspector
    public EnemySpawner spawner;
    public CharacterSkeleton player;

    private EnemyAttack attack;
    private int currentHealth;
    private bool dead= false;

    void Start()
    {
        attack = GetComponent<EnemyAttack>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterSkeleton>();

        if (difficulty == 1)
        {
            currentHealth = 100;
        }
        else if (difficulty == 2) 
        {
            currentHealth = 200;
        } else currentHealth = 50;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        particleEffect.Play();
        StartCoroutine(StopParticlesAfter(1.5f)); //Stops after 1.5s
        Debug.Log("Enemy took damage! Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            attack.enabled = false;
            if (!dead)
            {
            dead = true;
            StartCoroutine(Die());
            }
        } else soundSource.PlayOneShot(clipHit);
    }

    private IEnumerator StopParticlesAfter(float time)
    {
        yield return new WaitForSeconds(time);
        if (particleEffect != null)
        {
            particleEffect.Stop();
        }
    }

    private IEnumerator Die()
    {
        KillCounterManager.Instance.UpdateKillCount();
        soundSource.PlayOneShot(clipDie);
        Debug.Log("Enemy defeated!");
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject); // Or play a death animation
        spawner.enemiesKilled++;
        if(spawner.enemiesKilled >= spawner.numberOfEnemies)
        {
            RoundManager.Instance.NextRound();
        }
    }
}
