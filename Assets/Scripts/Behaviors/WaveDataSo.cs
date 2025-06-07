using System.Collections.Generic;
using UnityEngine;
namespace Behaviors
{
    [CreateAssetMenu(fileName = "NewWaveData", menuName = "Wave System/Wave Data")]
    public class WaveDataSo : ScriptableObject
    {
        public List<ListOfWaves> waves;
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            for (int i = 0; i < waves.Count; i++)
            {
                waves[i].waveName = $"Wave {i + 1}";
            }
        }
#endif
    }

    [System.Serializable]
    public class ListOfWaves
    {
        [HideInInspector] public string waveName;

        public float spawnFreq;
        public float timeDelay;
        public int maxSpawnNum;
        public int spawnedCount;
        public WaveEnemys waveEnemys;
    }

    [System.Serializable]
    public class WaveEnemys
    {
        public List<GameObject> enemySpawnList;
        public List<GameObject> spawnedEnemies;
    }
}