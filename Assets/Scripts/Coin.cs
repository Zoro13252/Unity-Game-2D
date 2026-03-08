using UnityEngine;

public class Coin : MonoBehaviour
{
    // Пустой: вся логика ушла в Player
    // Можно добавить: ParticleSystem, звук, вращение
    void Update()
    {
        transform.Rotate(0, 0, 90 * Time.deltaTime);  // ← опционально: вращение
    }
}