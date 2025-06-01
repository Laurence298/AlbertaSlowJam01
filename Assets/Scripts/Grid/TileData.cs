using UnityEngine;
using UnityEngine.Tilemaps;

namespace Grid
{
    [CreateAssetMenu(fileName = "Tile Data", menuName = "MENUNAME", order = 0)]
    public class TileData : ScriptableObject
    {
        public TileBase[] tiles;
        public tileType tileType;
    }
}