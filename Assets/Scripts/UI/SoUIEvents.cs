using Grid;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    [CreateAssetMenu(fileName = "UIEvent", menuName = "UI Events", order = 0)]
    public class SoUIEvents : ScriptableObject
    {
        public event UnityAction<UnitType> OnClickUnitUI;

        public event UnityAction OnPaintSelectedUI;
        public event UnityAction<int,int> OnWaveChanged;
        public event UnityAction OnStartPressed;
        public event UnityAction<float> OnTimerChanged;
        
        public event UnityAction OnGameStart;
        public GameState GameState;
        public event UnityAction<GameState> OnGameStateChanged;

        public void RaiseClickUnit(UnitType unitType)
        {
            OnClickUnitUI?.Invoke(unitType);
        }
        
        
        public void RaisePaintSelectedUI( )
        {
            OnPaintSelectedUI?.Invoke();
        }
        
        public void RaiseWaveChanged(int waveIndex , int maxWaveCount)
        {
            OnWaveChanged?.Invoke(waveIndex, maxWaveCount);
        }
        public void RaiseReadyPressed( )
        {
            OnStartPressed?.Invoke();
        }
        public void RaiseTimerChanged(float timer )
        {
            OnTimerChanged?.Invoke(timer);
        }
        public void RaiseGameStart( )
        {
            OnGameStart?.Invoke();
        }
        public void RaiseGameStateChanged(GameState state )
        {
            OnGameStateChanged?.Invoke(state);
        }

    }
}