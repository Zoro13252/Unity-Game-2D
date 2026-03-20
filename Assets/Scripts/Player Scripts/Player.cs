using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float attackRate = 1f;
    private float nextAttackTime = 0f;
    public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask enemyLayers;

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

        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= nextAttackTime)
        {
            playerAnimator.PlayAttack();
            nextAttackTime = Time.time + attackRate;
        }



        Move();
        JumpAnimation();
    }

    private void FixedUpdate()
    {
        // if (playerAnimator.IsInState("Attack"))
        // {
        //     rb.velocity = new Vector2(0, rb.velocity.y);
        //     return;
        // }
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay((Vector2)transform.position + Vector2.down * 0.1f, Vector2.down * 1f);
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
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
        if (isGrounded == false)
        {
            playerAnimator.IsJumping = true;
        }
        else
        {
            playerAnimator.IsJumping = false;
        }
    }

    /// Attack logic
    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy != null)
            {
                FirstEnemy enemyScript = enemy.GetComponent<FirstEnemy>();
                if (enemyScript != null)
                {
                    enemyScript.enemyTakeDamage(damage);
                }
                else
                {
                    Debug.LogError("enemyScript = null");
                }
            }
        }
    }
}