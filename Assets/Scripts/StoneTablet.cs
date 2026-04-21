using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoneTablet : MonoBehaviour
{

    [SerializeField] TextMeshPro textMesh;

    void Awake()
    {
        if (textMesh == null)
        {
            Debug.LogError("TextMesh не назначен в инспекторе на StoneTablet!", this);
            return;
        }

        textMesh.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textMesh.enabled = true;
        }
    }
}
