using Unity.VisualScripting;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField] PlayerHealth playerHealth;
    private string bonusName;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // switch (bonusName)
            // {
            //     // case "healthPotion":
            //     //     playerHealth.AddHealth();
            //     // break;
            // }
        }
    }
}
