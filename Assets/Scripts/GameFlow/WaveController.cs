using System;
using System.Collections.Generic;
using System.Linq;
using Behaviors;
using UI;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Splines;

namespace GameFlow
{
    public class WaveController : MonoBehaviour
    {
        public int MaxWaves;
        public int CurrentWaves;
        
        [FormerlySerializedAs("uiEvents")] public SoUIEvents soUIEvents;
        public EnemySpawner[] EnemySpawners;
        public SplineContainer left;
        public SplineContainer right;
        public WaveDataSo[] wavedata;

        public bool waveInProgress;

        private void Start()
        {
          
        }

       public void ReadyUp()
        {
            CurrentWaves = 0;
            EnemySpawners = FindObjectsByType<EnemySpawner>(FindObjectsSortMode.None);
            soUIEvents.RaiseWaveChanged(CurrentWaves, MaxWaves);

            if (EnemySpawners.Length == 2)
            {
                var spawnerA = EnemySpawners[0];
                var spawnerB = EnemySpawners[1];

                if (spawnerA.transform.position.x < spawnerB.transform.position.x)
                {
                    spawnerA.InitROute( left);
                    spawnerB.InitROute(right);
                }
                else
                {
                    spawnerA.InitROute(right);
                    spawnerB.InitROute(left);
                }
            }

            for (int i = 0; i < wavedata.Length; i++)
            {
                EnemySpawners[i].InitSpawner(this, wavedata[i]);
            }

         
        }
        

        public void StartWave(bool start)
        {
            waveInProgress = start;
        }

        public bool IsWaveInProgress()
        {
            return waveInProgress;
        }
        public void GetSpawnPoints()
        {
            
        }

        public void NextWave()
        {
            CurrentWaves++;
            soUIEvents.RaiseWaveChanged(CurrentWaves, MaxWaves);

        }

        public bool AreAllEnemiesDead()
        {
            foreach (EnemySpawner spawner in EnemySpawners)
            {
                if(!spawner.AreAllEnemiesDead())
                    return false;
            }
            
            return true;
        }
        public bool LevelCompleted()
        {
            if (CurrentWaves >= MaxWaves && !AreAllEnemiesDead())
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