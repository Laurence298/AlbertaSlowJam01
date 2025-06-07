using System;
using UnityEngine;
using TMPro;
using UI;

public class CoinsUI : MonoBehaviour
{
    private TMP_Text _coinText;
    public MoneyCounter somoney;

    private void Awake()
    {
        somoney.OnCoinsChanged += SomoneyOnOnCoinsChanged;
    }

    private void SomoneyOnOnCoinsChanged(int arg0)
    {
        throw new NotImplementedException();
    }

    private void Start()
    {
        throw new NotImplementedException();
    }

    public void UpdateScore(MoneyCounter Coins) 
    {
        _coinText.text = $"Score: {Coins.CoinCount}";
    }
}
