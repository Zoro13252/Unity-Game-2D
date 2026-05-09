using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoneTablet : MonoBehaviour
{

    [SerializeField] TextMeshPro textMesh;
    [SerializeField] TextAnimation textAnimation;

    void Awake()
    {
        if (textMesh == null)
        {
            Debug.LogError("TextMesh не назначен в инспекторе на StoneTablet!", this);
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textAnimation.StartAnimation();
        }
    }
}
