using System;
using UnityEngine;

public class EnemyLoot : MonoBehaviour
{
    public MoneyCounter moneyCounter;

    public int Coin = 2;
    private void OnDestroy()
    {
        moneyCounter.AddScore(Coin);
    }
}
