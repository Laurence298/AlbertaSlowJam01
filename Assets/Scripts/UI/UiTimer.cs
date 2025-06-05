using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class UiTimer : MonoBehaviour
    {
        [FormerlySerializedAs("uiTimer")] public SoUIEvents soUITimer;
        public TextMeshProUGUI timerText;

        private void Start()
        {
            soUITimer.OnTimerChanged += SoUITimerOnOnTimerChanged;
        }

        private void SoUITimerOnOnTimerChanged(float timer)
        {
            if (timer > 0)
            {
                timerText.enabled = true;
                timerText.text = timer.ToString("0");

            }
            else
            {
                timerText.enabled = false;
            }
        }
    }
}