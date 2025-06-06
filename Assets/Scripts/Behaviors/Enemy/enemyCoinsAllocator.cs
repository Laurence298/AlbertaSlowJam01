using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemycoinsAllocator : MonoBehaviour
{
    [SerializeField]
    private int _killCoins;

    private ScoreController _coins;

    private void Awake()
    {
        _coins = FindObjectOfType<ScoreController>();

    }

    public void AllocatedCoins()
    {
        _coins.AddScore(_killCoins);
    }
}
