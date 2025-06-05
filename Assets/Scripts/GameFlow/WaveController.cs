using System;
using System.Collections.Generic;
using System.Linq;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameFlow
{
    public class WaveController : MonoBehaviour
    {
        public int MaxWaves;
        public int CurrentWaves;
        
        [FormerlySerializedAs("uiEvents")] public SoUIEvents soUIEvents;


        private void Start()
        {
            CurrentWaves = 0;
            soUIEvents.RaiseWaveChanged(CurrentWaves, MaxWaves);
        }

        public void StartWave()
        {
            
        }

        public void GetSpawnPoints()
        {
            
        }

        public void NextWave()
        {
            CurrentWaves++;
            soUIEvents.RaiseWaveChanged(CurrentWaves, MaxWaves);

        }

        public bool AreEnemiesAlive()
        {
            return false;
        }
        public bool LevelCompleted()
        {
            if (CurrentWaves >= MaxWaves && !AreEnemiesAlive())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}