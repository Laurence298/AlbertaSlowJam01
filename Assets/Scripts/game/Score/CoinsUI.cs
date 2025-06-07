using UnityEngine;
using TMPro;

public class CoinsUI : MonoBehaviour
{
    private TMP_Text _coinText;

    private void Awake()
    {
        _coinText = GetComponent<TMP_Text>();
    }

    public void UpdateScore(MoneyCounter Coins) 
    {
        _coinText.text = $"Score: {Coins.CoinCount}";
    }
}
