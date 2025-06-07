using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class ReadyButton : MonoBehaviour
    {
        public SoUIEvents soUIEvents;
        private GameState gameState;
        public Button button;
        public float duration = 1f;
        public float lowAlpha = 0.3f;
        public float highAlpha = 1f;
        public bool fadeIn = true; 
        [FormerlySerializedAs("hasDeacreased")] public bool hasIncreased;

        public Coroutine saturation;
  

        private void Awake()
        {
            soUIEvents.OnGameStateChanged += SoUIEventsOnOnGameStateChanged;

        }

  

        private void OnDestroy()
        {
            soUIEvents.OnGameStateChanged -= SoUIEventsOnOnGameStateChanged;

        }

        private void SoUIEventsOnOnGameStateChanged(GameState arg0)
        {

            if (arg0 == GameState.Preaping)
            {
                hasIncreased = true;

                TriggerSaturation(true);
            }

            if (arg0 != GameState.Preaping && hasIncreased)
            {
                hasIncreased = false;
                TriggerSaturation(false);

            }
            
            
            gameState = arg0;
            
            
        }

     
        public void TriggerSaturation(bool toVisible)
        {
            if(saturation != null)
                StopCoroutine(saturation);
          saturation =  StartCoroutine(LerpSaturation(toVisible));
        }

        IEnumerator LerpSaturation(bool toVisible)
        {
            Image img = button.image;
            Color startColor = img.color;
            float startAlpha = startColor.a;
            float targetAlpha = toVisible ? highAlpha : lowAlpha;

            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / duration);
                float currentAlpha = Mathf.Lerp(startAlpha, targetAlpha, t);
                img.color = new Color(startColor.r, startColor.g, startColor.b, currentAlpha);
                yield return null;
            }

            // Snap to final alpha
            img.color = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);
        }
        public void ReadyPressed()
        {
            if(gameState == GameState.Preaping)
                soUIEvents.RaiseReadyPressed();
        }
    }
}