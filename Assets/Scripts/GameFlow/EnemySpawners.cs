using System.Collections.Generic;
using UnityEngine;

namespace GameFlow
{
    public class EnemySpawners : MonoBehaviour
    {
        public List<Transform> DestinationRoot;
        public List<EnemyWaves> waveData;
        
        
        private void OnValidate()
        {
            for (int i = 0; i < waveData.Count; i++)
            {
                if (waveData[i] != null)
                {
                    waveData[i].waveName = $"Wave {i + 1}";
                }
            }
        }
    }
    [System.Serializable]
    public class EnemyWaves
    {
        public string waveName;
        public List<WaveData> waveData;
    }

    [System.Serializable]
    public class WaveData
    {
      
        public GameObject enemy;
        public int count;
    }
    
}