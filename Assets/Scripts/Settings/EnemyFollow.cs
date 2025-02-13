using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private Transform player;
    public float moveSpeed = 3f;
    private Animator animator;

    void Start()
    {
        FindPlayer(); // Ensure the player is assigned at the start
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null)
        {
            Debug.Log("There is a player");
            FindPlayer(); // Reassign the player if null (fixes issue on scene reload)
        }

        FollowPlayer();
    }

    void FindPlayer()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            Debug.Log("Player found");
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player not found! Make sure the player is tagged as 'Player'.");
        }
    }

    void FollowPlayer()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0f; // Keep movement on the horizontal plane
            animator.SetBool("isWalking", true);

            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }
    }
}
