using UnityEngine;
using UnityEngine.Events;

namespace Grid
{
    [CreateAssetMenu(menuName = "Grid/SOGridEvents", fileName = "SOGridEvents")]
    public class SOGridEvents : ScriptableObject
    {
        public event UnityAction<Vector3> OnMousPointerPositionChanged;
        
        public event UnityAction<PointerStates> OnpointerTypeChanged;

        public void OnMousePositionChanged(Vector3 position)
        {
            OnMousPointerPositionChanged?.Invoke(position);
        }
    }
}