using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private Transform player;
    public float moveSpeed = 3f;
    private Animator animator;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform; // Ensure your player is tagged as "Player"
        animator = GetComponent<Animator>();

        if (animator.runtimeAnimatorController != null)
        {
            animator.SetBool("walk", true); // For Mecanim animations
        }
        else if (GetComponent<Animation>() != null)
        {
            GetComponent<Animation>().Play("walk"); // For Legacy animations
        }
    }

    void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0f; // Keep movement on the horizontal plane

            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }
    }
}
