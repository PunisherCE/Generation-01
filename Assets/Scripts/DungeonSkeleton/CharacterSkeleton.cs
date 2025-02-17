using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSkeleton : MonoBehaviour
{
    [SerializeField] AudioSource soundSource;
    [SerializeField] AudioClip clipHit;
    [SerializeField] AudioClip clipDie;

    public ParticleSystem particleEffect; // Assign this in Inspector
    public Transform cameraTransform;
    public Image healthBarFill; // Assign in Inspector
    public LayerMask groundLayer;
    public int health = 100;
    public int maxHealth = 100;
    public float gravity = 9.81f;
    public float rotationSpeed = 15f;
    public SwordDamage sword;

    private float moveSpeed = 8f;
    private float sprintSpeed = 12f;
    private float jumpForce = 9f;
    private bool isGrounded;
    private bool gameStatus = true;
    private CharacterController controller;
    private Vector3 velocity;
    private Animator animator;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction attackAction;
    private InputAction sprintAction;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        attackAction = playerInput.actions["Attack"];
        sprintAction = playerInput.actions["Sprint"]; // Ensure Sprint is mapped in Input Actions
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Reset vertical velocity when grounded
        }

        HandleAnimations();
        Move();
        Jump();
        ApplyGravity();

        if (!gameStatus) animator.SetTrigger("Fall1");
    }

    void Move()
    {
        if (gameStatus)
        {
            Vector2 input = moveAction.ReadValue<Vector2>();
            float horizontal = input.x;
            float vertical = input.y;

            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            float currentSpeed = sprintAction.IsPressed() ? sprintSpeed : moveSpeed;

            if (direction.magnitude >= 0.1f)
            {
                animator.SetBool("isWalking", true);
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
                float angle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, Time.deltaTime * rotationSpeed);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDirection.normalized * currentSpeed * Time.deltaTime);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }

    }

    void Jump()
    {
        if (jumpAction.triggered && isGrounded && gameStatus)
        {
            velocity.y = jumpForce;
            animator.SetTrigger("Up");
        }
    }

    void ApplyGravity()
    {
        velocity.y -= gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleAnimations()
    {
        if (attackAction.triggered && gameStatus)
        {
            if (isGrounded)
            {
                animator.SetTrigger("Attack1h1");
                StartCoroutine(HandleSwordDamage(10));
            } else
            {
                animator.SetTrigger("Attack1h1");
                StartCoroutine(HandleSwordDamage(20));
            }
        }
    }

    System.Collections.IEnumerator HandleSwordDamage(int damage)
    {
        sword.EnableDamage(damage);
        yield return new WaitForSeconds(1.2f); // Wait for 1 second
        sword.DisableDamage();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        UpdateHealthUI();
        particleEffect.Play();
        StartCoroutine(StopParticlesAfter(1.5f)); //Stops after 1.5s

        Debug.Log("Health: " + health);

        if (health < 1 && gameStatus)
        {
            soundSource.PlayOneShot(clipDie);
            gameStatus = false;
            StartCoroutine(RestartScene());
        }
        else
        {
            soundSource.PlayOneShot(clipHit);
        }
    }

    //Coroutine to stop particles
    private IEnumerator StopParticlesAfter(float time)
    {
        yield return new WaitForSeconds(time);
        if (particleEffect != null)
        {
            particleEffect.Stop();
        }
    }


    private IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(2f); // Wait 2 seconds
        SceneManager.LoadScene("Arena");
    }
    public void UpdateHealthUI()
    {
        if (healthBarFill != null)
        {
            float healthPercentage = (float)health / maxHealth;
            healthBarFill.fillAmount = healthPercentage;

            // Transition from green (healthy) to red (low health)
            healthBarFill.color = Color.Lerp(Color.red, Color.green, healthPercentage);
        }
    }

}

