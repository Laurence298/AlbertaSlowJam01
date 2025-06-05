using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class PaintUI : MonoBehaviour
    {
        [FormerlySerializedAs("uiEvents")] public SoUIEvents soUIEvents;


        public void PaintSelected()
        {
            soUIEvents.RaisePaintSelectedUI();
        }
    }
}