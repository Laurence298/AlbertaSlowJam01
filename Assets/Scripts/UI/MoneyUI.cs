using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class MoneyUI : MonoBehaviour
    {
        public TextMeshProUGUI MoneyText;
        public MoneyCounter moneyCounter;

        private void Awake()
        {
            moneyCounter.OnCoinsChanged += MoneyCounterOnOnCoinsChanged;
        }

        private void OnDestroy()
        {
            moneyCounter.OnCoinsChanged -= MoneyCounterOnOnCoinsChanged;
        }

        private void MoneyCounterOnOnCoinsChanged(int arg0)
        {
            Debug.Log("chaning money");
           MoneyText.text = arg0.ToString();
        }
    }
}