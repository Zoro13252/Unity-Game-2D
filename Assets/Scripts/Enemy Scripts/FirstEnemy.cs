using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstEnemy : MonoBehaviour
{
    [Header("Base settings")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float maxHealth = 1f;
    [SerializeField] private float enemyDamage = 1f;
    [SerializeField] Transform[] Point = new Transform[2];
    [SerializeField] private PlayerHealth playerHealth;
    private float currentHealth;
    Rigidbody2D rb;
    SpriteRenderer sprEnemy;
    private bool movingRight = true;
    private EnemyAnimator enemyAnimator;

    [Header("Attack settings")]
    public Transform attackZone;
    public float enemyAttackRange;
    public LayerMask playerLayers;
    public float inspectionZoneDiameter; //<-- search for a player within a certain radius
    public Transform searchZone;
    public Transform player;



    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprEnemy = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;

        enemyAnimator = GetComponent<EnemyAnimator>();

        // ←←← Это очень важно!
        if (enemyAnimator == null)
        {
            Debug.LogError($"[ERROR] На объекте {gameObject.name} отсутствует компонент EnemyAnimator! Добавь его.", this);
        }

        if (playerHealth == null)
        {
            playerHealth = FindObjectOfType<PlayerHealth>();
            if (playerHealth == null)
                Debug.LogWarning("PlayerHealth не найден на сцене!");
        }
    }

    void FixedUpdate()
    {
        if (movingRight)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            if (transform.position.x >= Point[1].position.x)
            {
                enemyAnimator.IsRunning = true;
                movingRight = false;
                sprEnemy.flipX = true;
            }
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            if (transform.position.x <= Point[0].position.x)
            {
                enemyAnimator.IsRunning = true;
                movingRight = true;
                sprEnemy.flipX = false;
            }
        }

        PlayerSearch();
        Agressiv();
    }




    public void enemyTakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void EnemyAttack()
    {
        if (playerHealth == null) return;
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackZone.position, enemyAttackRange, playerLayers);

        foreach (Collider2D item in hitPlayer)
        {
            if (item != null)
            {
                playerHealth.TakeDamage(enemyDamage);
            }
        }
    }


    private void PlayerSearch()
    {

        if (enemyAnimator == null)
        {
            Debug.LogError("EnemyAnimator is missing on " + gameObject.name);
            return;
        }

        Collider2D playerInZone = Physics2D.OverlapCircle(searchZone.position, inspectionZoneDiameter, playerLayers);
        bool playerFound = playerInZone != null;

        enemyAnimator.IsAttacking = playerFound;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackZone.position, enemyAttackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(searchZone.position, inspectionZoneDiameter);
    }

    void Agressiv()
    {

    }
}
