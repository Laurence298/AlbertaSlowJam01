using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class ReadyButton : MonoBehaviour
    {
        public SoUIEvents soUIEvents;

        private void Start()
        {
         
        }

        public void ReadyPressed()
        {
            soUIEvents.RaiseReadyPressed();
        }
    }
}