using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 3f;
    [Header("UI")]
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text coinText;

    public float health;
    private GameManager gameManager;
    private Player player;

    void Awake()
    {
        health = maxHealth;
        player = GetComponent<Player>();
        gameManager = GameManager.Instance;

        UpdateUI();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        health = Mathf.Max(0, health);

        UpdateUI();

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (gameManager != null && player != null)
        {
            gameManager.AddCoin(player.coinCount);
            Debug.Log($"Добавлено в GameManager: {player.coinCount} монет");
        }

        gameObject.SetActive(false);
    }

    private void UpdateUI()
    {
        if (healthText != null)
            healthText.text = $"HP: {Mathf.RoundToInt(health)}/{Mathf.RoundToInt(maxHealth)}";
    }

    private void Update()
    {
        if (coinText != null && player != null)
            coinText.text = $"Coins: {player.coinCount}";
    }
}