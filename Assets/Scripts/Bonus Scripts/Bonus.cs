using Unity.VisualScripting;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] Player player;
    public string bonusName;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            switch (bonusName)
            {
                case "healthPotion":
                    if(playerHealth.health >= 100)
                    {
                        return;
                    }
                    else
                    {
                        playerHealth.health += 15;
                        playerHealth.UpdateUI();
                        Destroy(gameObject);
                    }
                break;

                case "attackPotion":
                    player.damage += 3;
                break;
            }
        }
    }
}
