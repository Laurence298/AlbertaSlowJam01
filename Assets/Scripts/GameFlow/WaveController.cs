using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameFlow
{
    public class WaveController : MonoBehaviour
    {
        public int MaxWaves;
        public int CurrentWaves;
        
        public List<EnemySpawners> spawners;


        private void Start()
        {
            spawners = transform.GetComponentsInChildren<EnemySpawners>().ToList();
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
        }

        public bool CheckForEnemies()
        {
            return false;
        }
        public bool LevelCompleted()
        {
            if (CurrentWaves > MaxWaves && !CheckForEnemies())
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