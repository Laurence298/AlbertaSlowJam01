using System;
using Grid;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class UnitUI : MonoBehaviour
    {
        public UnitType unitType;
        public SoUIEvents events;
        public Button button;
        public TextMeshProUGUI costText;
        public MoneyCounter moneyCounter;

        private void Awake()
        {
        
        }

        private void Start()
        {
            button.onClick.AddListener(ReturnUnitType);
            costText.text = moneyCounter.unitperCost[unitType].ToString();
            moneyCounter.OnCoinsChanged += MoneyCounterOnOnCoinsChanged;
            AddHoverEvents();
        }

        private void OnDisable()
        {
            moneyCounter.OnCoinsChanged -= MoneyCounterOnOnCoinsChanged;

        }

        private void MoneyCounterOnOnCoinsChanged(int arg0)
        {

            if (moneyCounter.unitperCost[unitType] < arg0)
            {
                costText.color = Color.white;
            }
            else
            {
                costText.color = Color.red;
            }
        }

        private void AddHoverEvents()
        {
            EventTrigger trigger = GetComponent<EventTrigger>();

            // Pointer Enter
            EventTrigger.Entry entryEnter = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerEnter
            };
            entryEnter.callback.AddListener((eventData) => OnHoverEnter());
            trigger.triggers.Add(entryEnter);

            // Pointer Exit (optional)
            EventTrigger.Entry entryExit = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerExit
            };
            entryExit.callback.AddListener((eventData) => OnHoverExit());
            trigger.triggers.Add(entryExit);
        }

        private void OnHoverEnter()
        {
            Debug.Log($"Hovered on button for {unitType}");
            // Play sound, change color, animate, etc.
        }

        private void OnHoverExit()
        {
            Debug.Log($"Exited hover on button for {unitType}");
        }

        public void ReturnUnitType()
        {
           events.RaiseClickUnit(unitType);
        }
    }
}