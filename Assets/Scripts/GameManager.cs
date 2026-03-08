using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    int coinCount = 0;
    private void Awake()
    {
        Instance = this;
    }
    // public void Die()
    // {
    //     gameObject.SetActive(false);
    // }

    public void AddCoin(int coin)
    {
        coinCount += coin;
    }
}


