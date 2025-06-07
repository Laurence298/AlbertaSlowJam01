using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Grid.Tests
{
    [CreateAssetMenu(fileName = "New Unit List", menuName = "Unit List")]
    public class UnitList : ScriptableObject
    {
        [SerializedDictionary("unity type","Unit")]
        public SerializedDictionary<UnitType,UnitData> units;
    }

    [System.Serializable]
    public class UnitData
    {
        public GameObject unit;
        public TileData tileData;
    }
}