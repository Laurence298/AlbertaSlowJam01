using System;
using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Grid;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Money", menuName = "MoneyController")]
public class MoneyCounter : ScriptableObject
{
    public event UnityAction<int> OnCoinsChanged;
    [SerializedDictionary("unit","cost")]
    public SerializedDictionary<UnitType, int> unitperCost;
    public int grassCost;
    private int grassPurchesedCount;
    public int CoinCount { get; private set; }
    public int startingAmount;


    private void OnEnable()
    {
        CoinCount = 0;
    }

    public void StartingAMoung()
    {
        CoinCount = startingAmount;
        OnCoinsChanged?.Invoke(startingAmount);
    }


    public void AddScore(int amount)
    {
        CoinCount += amount;
        OnCoinsChanged.Invoke(CoinCount);
    }

    public void SubractScore(int amount)
    {
        CoinCount -= amount;
        OnCoinsChanged.Invoke(CoinCount);
    }

    public bool CanPurchaseUnit( UnitType unitType)
    {
        if (CoinCount >= unitperCost[unitType])
        {
           return true;
                
        }
        return false;
    }
    public void PurchaseUnit( UnitType unitType)
    {
        if (CoinCount >= unitperCost[unitType])
        {
            SubractScore(unitperCost[unitType]);
                
        }
    }
    public bool CanPurchaseGrass(  )
    {
        if (grassCost <= CoinCount)
        {
            return true;
                
        }
        return false;
    }
    public void PurchaseGrass(  )
    {
        if (grassCost <= CoinCount)
        {
            SubractScore(grassCost);
                
        }
      
    }
}

