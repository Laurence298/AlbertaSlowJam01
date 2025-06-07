using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class PaintUI : MonoBehaviour
    {
        [FormerlySerializedAs("uiEvents")] public SoUIEvents soUIEvents;
        public TextMeshProUGUI cost;
        public MoneyCounter moneyCounter;

        private void Awake()
        {
            cost.text = moneyCounter.grassCost.ToString();
            moneyCounter.OnCoinsChanged += MoneyCounterOnOnCoinsChanged;

        }
        private void OnDisable()
        {
            moneyCounter.OnCoinsChanged -= MoneyCounterOnOnCoinsChanged;

        }

        private void MoneyCounterOnOnCoinsChanged(int arg0)
        {

            if (moneyCounter.grassCost <= arg0)
            {
                cost.color = Color.white;
            }
            else
            {
                cost.color = Color.red;
            }
        }
        public void PaintSelected()
        {
            soUIEvents.RaisePaintSelectedUI();
        }
    }
}