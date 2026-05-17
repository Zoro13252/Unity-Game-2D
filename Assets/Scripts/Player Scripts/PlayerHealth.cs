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
    private PlayerAnimator playerAnimator;

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
        // Защита от null
        if (gameManager == null)
        {
            gameManager = GameManager.Instance;
        }

        if (gameManager != null && player != null)
        {
            gameManager.SaveCoin(player.coinCount);
            Debug.Log($"Добавлено в GameManager: {player.coinCount} монет");
        }
        else
        {
            Debug.LogWarning("GameManager не найден! Монеты не сохранены.");
            
            // Альтернативное сохранение напрямую
            if (player != null)
            {
                PlayerPrefs.SetInt("Coin", player.coinCount);
                PlayerPrefs.Save();
            }
        }

        gameObject.SetActive(false);
        // Time.timeScale = 0;  // раскомментируй, если нужно остановить игру
    }

    public void UpdateUI()
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