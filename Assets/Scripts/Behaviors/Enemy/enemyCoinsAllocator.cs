using UnityEngine;
using UnityEngine.InputSystem; // Required for Mouse.current
using System.Collections;

public class enemycoinsAllocator : MonoBehaviour
{
    private int _killCoins;

    private ScoreController _coins;

    public int ItemAmount = 10; // This is the item price

    private void Awake()
    {
        _coins = FindObjectOfType<ScoreController>();
    }

    public void AllocatedCoins()
    {
        _coins.AddScore(_killCoins);
    }

    // Method to try to "buy" an item
    public void CurrencyExchangeItem()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (_killCoins >= ItemAmount)
            {
                _killCoins -= ItemAmount;
                Debug.Log("Item purchased! Remaining coins: " + _killCoins);
            }
            else
            {
                Debug.Log("Not enough coins to purchase the item.");

                // Do nothing, leave _killCoins as is
            }
        }
    }
}
