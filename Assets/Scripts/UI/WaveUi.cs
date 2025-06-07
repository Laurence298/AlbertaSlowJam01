using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class WaveUi : MonoBehaviour
    {
        [FormerlySerializedAs("UIEvents")] public SoUIEvents soUIEvents;
        public TextMeshProUGUI text;


        private void Start()
        {
            soUIEvents.OnWaveChanged += SoUIEventsOnOnWaveChanged;
        }

        private void OnDestroy()
        {
            soUIEvents.OnWaveChanged -= SoUIEventsOnOnWaveChanged;

        }

        private void SoUIEventsOnOnWaveChanged(int currentWave, int maxwave)
        {
            text.text = currentWave.ToString() + " / " + maxwave.ToString();
        }
    }
}