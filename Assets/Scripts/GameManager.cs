using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }


    public void SaveCoin(int coins)
    {
        PlayerPrefs.SetInt("Coin", coins);
    }

    public int LoadCoin()
    {
        return PlayerPrefs.GetInt("Coin");
    }


    // public void AddCoin(int coin)
    // {
    //     coinCount += coin;
    // }
}


