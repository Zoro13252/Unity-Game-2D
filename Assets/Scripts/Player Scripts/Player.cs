using Unity.VisualScripting;
using UnityEngine;

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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerHealth = GetComponent<PlayerHealth>();
        if (playerHealth == null) Debug.LogError("playerHealth = null");
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.Raycast((Vector2)transform.position + Vector2.down * 0.1f, Vector2.down, 0.7f, groundLayer);


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.color = Color.red;
        Gizmos.DrawRay((Vector2)transform.position + Vector2.down * 0.1f, Vector2.down * 0.7f);
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
}