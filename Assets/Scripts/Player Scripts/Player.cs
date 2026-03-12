using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Coins")]
    public int coinCount = 0;
    private Rigidbody2D rb;
    private bool isGrounded;
    private float horizontalInput;
    private PlayerHealth playerHealth;
    private SpriteRenderer spriteRenderer;
    private PlayerAnimator playerAnimator;
    private bool isMoving;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerHealth = GetComponent<PlayerHealth>();
        if (playerHealth == null) Debug.LogError("playerHealth = null");
        playerAnimator = GetComponent<PlayerAnimator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.Raycast((Vector2)transform.position + Vector2.down * 0.1f, Vector2.down, 1f, groundLayer);


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
        Move();
        JumpAnimation();
        Attack();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.color = Color.red;
        Gizmos.DrawRay((Vector2)transform.position + Vector2.down * 0.1f, Vector2.down * 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            FirstEnemy enemyScript = collision.GetComponent<FirstEnemy>();
            if (enemyScript != null)
            {
                enemyScript.enemyTakeDamage(damage);
            }
        }

        if (collision.CompareTag("Coin"))
        {
            coinCount++;
            Debug.Log($"Собрано монет: {coinCount}");
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            playerHealth.Die();
        }
    }

    private void Move()
    {
        isMoving = horizontalInput != 0 ? true : false;

        if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
        }

        playerAnimator.IsMoving = isMoving;

    }

    private void JumpAnimation()
    {
        if (isGrounded != true)
        {
            playerAnimator.IsJumping = true;
        }
        else if (isGrounded == true)
        {
            playerAnimator.IsJumping = false;
        }
        else if (isGrounded != true && rb.velocity.y < -0.1f)
        {
            playerAnimator.IsJumping = false;
            playerAnimator.IsJumpingToFall = true;
        }
        else if (isGrounded == true)
        {
            playerAnimator.IsJumpingToFall = false;
        }


    }
    int i = 0;
    private void Attack()
    {

        if (i < 5)
        {
            playerAnimator.Attack = true;
            i++;
        }
    }
}