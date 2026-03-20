using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstEnemy : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float maxHealth = 1f;
    [SerializeField] private float enemyDamage = 1f;
    [SerializeField] Transform[] Point = new Transform[2];
    [SerializeField] private PlayerHealth playerHealth;
    public float currentHealth;
    Rigidbody2D rb;
    SpriteRenderer SprEnemy;
    public CapsuleCollider2D capsule;

    private bool movingRight = true;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        SprEnemy = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        capsule = GetComponent<CapsuleCollider2D>();
    }

    void FixedUpdate()
    {
        if (movingRight)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            if (transform.position.x >= Point[1].position.x)
            {
                movingRight = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            if (transform.position.x <= Point[0].position.x)
            {
                movingRight = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(enemyDamage);
            }

        }
    }


    public void enemyTakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }




}


