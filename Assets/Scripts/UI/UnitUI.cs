using System;
using Grid;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UnitUI : MonoBehaviour
    {
        public UnitType unitType;
        public SoUIEvents events;

        

        public void ReturnUnitType()
        {
           events.RaiseClickUnit(unitType);
        }
    }
}