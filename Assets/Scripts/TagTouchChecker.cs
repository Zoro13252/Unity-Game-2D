using System.Collections.Generic;
using UnityEngine;

public class TagTouchChecker : MonoBehaviour
{
    [Header("Настройки проверок")]
    [SerializeField] private bool checkTriggers = true;  // Проверять триггеры? (true для платформеров)
    [SerializeField] private LayerMask contactLayers = -1;  // Какие слои проверять (по умолчанию все)

    private Rigidbody2D rb;
    private ContactFilter2D contactFilter;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("TagTouchChecker требует Rigidbody2D на объекте!");
            enabled = false;
            return;
        }

        // Настраиваем фильтр контактов
        contactFilter.useTriggers = checkTriggers;
        contactFilter.SetLayerMask(contactLayers);
        contactFilter.useLayerMask = true;
    }

    /// <summary>
    /// Проверяет, касается ли объект с данным тегом (Collision или Trigger)
    /// </summary>
    public bool IsTouching(string tag)
    {
        Collider2D[] results = new Collider2D[10];  // Массив для до 10 контактов (расширь если нужно)
        int numContacts = rb.GetContacts(contactFilter, results);

        for (int i = 0; i < numContacts; i++)
        {
            if (results[i].CompareTag(tag))
            {
                return true;  // Нашёлся!
            }
        }
        return false;
    }

    /// <summary>
    /// Возвращает список ВСЕХ уникальных тегов, с которыми сейчас касается
    /// </summary>
    public List<string> GetAllTouchedTags()
    {
        HashSet<string> touchedTags = new HashSet<string>();
        Collider2D[] results = new Collider2D[20];
        int numContacts = rb.GetContacts(contactFilter, results);

        for (int i = 0; i < numContacts; i++)
        {
            touchedTags.Add(results[i].tag);
        }

        return new List<string>(touchedTags);
    }

    /// <summary>
    /// Сколько контактов всего сейчас?
    /// </summary>
    public int GetContactCount()
    {
        Collider2D[] results = new Collider2D[1];  // Не важен размер, считаем только количество
        return rb.GetContacts(contactFilter, results);
    }
}