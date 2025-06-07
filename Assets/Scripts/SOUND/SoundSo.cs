using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace DefaultNamespace.SOUND
{
    [CreateAssetMenu(menuName = "Small Hedge/Sounds SO", fileName = "Sounds SO")]
    public class SoundsSO : ScriptableObject
    {
        [SerializedDictionary("Sounds","data")]
        public SerializedDictionary<SoundType, SoundList> sounds;
        
    }
}