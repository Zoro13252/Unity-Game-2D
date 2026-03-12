using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private Transform player;     // ← перетащи сюда объект игрока в инспекторе
    [SerializeField] private float smoothSpeed = 0.125f;

    private void LateUpdate()
    {
        if (player == null) return;
        Vector2 desiredPosition = new Vector2(player.position.x, 0f);

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}