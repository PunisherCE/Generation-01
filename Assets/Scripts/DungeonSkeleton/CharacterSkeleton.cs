using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CharacterSkeleton : MonoBehaviour
{
    public Transform cameraTransform;
    public LayerMask groundLayer;
    public float gravity = 9.81f;
    public float rotationSpeed = 15f;
    public SwordDamage sword;
    public int health = 100;

    private float moveSpeed = 8f;
    private float sprintSpeed = 12f;
    private float jumpForce = 9f;
    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
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
        HandleAnimations();
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Reset vertical velocity when grounded
        }

        Move();
        Jump();
        ApplyGravity();
    }

    void Move()
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

    void Jump()
    {
        if (jumpAction.triggered && isGrounded)
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
        if (attackAction.triggered)
        {
            if (!isGrounded)
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
        Debug.Log("Health: " + health);

        if (health < 1)
        {
            SceneManager.LoadScene("Arena");
        }
    }
}
