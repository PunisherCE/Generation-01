// Enemy Attack Script
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform player; // Assign the player in the Inspector
    public float attackRange = 2f; // Distance at which the enemy starts attacking
    public int damageAmount = 5; // Reduced damage inflicted

    private CharacterSkeleton skeleton;
    private Animator animator;
    private EnemyFollow enemyFollow;
    private float attackCooldown = 1f; // Time between attacks
    private bool isAttacking = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        enemyFollow = GetComponent<EnemyFollow>(); // Reference to control movement
        player = GameObject.FindGameObjectWithTag("Player").transform; // Ensure player is tagged "Player"
        skeleton = player.GetComponent<CharacterSkeleton>(); // Correctly reference player's script
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange && !isAttacking)
        {
            StartCoroutine(PerformAttack());
        }
    }

    private void OnDisable()
    {
        if (enemyFollow != null)
        {
            enemyFollow.enabled = false;
        }
    }

    System.Collections.IEnumerator PerformAttack()
    {
        isAttacking = true;
        animator.SetTrigger("attack");
        animator.SetBool("isWalking", false);

        if (enemyFollow != null)
        {
            enemyFollow.enabled = false; // Stop movement while attacking
        }

        yield return new WaitForSeconds(0.5f); // Wait for the attack animation to hit

        DealDamage();

        yield return new WaitForSeconds(attackCooldown); // Wait for cooldown

        if (enemyFollow != null)
        {
            enemyFollow.enabled = true; // Resume movement after attack
        }

        isAttacking = false;
        animator.SetBool("isWalking", true);
    }

    void DealDamage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position + transform.forward * 1f + (transform.up * 1.25f), 1.3f);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                Debug.Log("Player hit by enemy");
                // Apply damage to player here
                skeleton.TakeDamage(damageAmount);

                if (skeleton.health < 1)
                {
                    this.enabled = false;
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward * 1f + (transform.up * 1.25f), 1.3f);
    }
}
