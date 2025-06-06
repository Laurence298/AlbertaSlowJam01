using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreController : MonoBehaviour
{
    public UnityEvent OnCoinsChanged;

    public int CoinScorer { get; private set; }


    public void AddScore(int amount)
    {
        CoinScorer += amount;
        OnCoinsChanged.Invoke();
    }
}
