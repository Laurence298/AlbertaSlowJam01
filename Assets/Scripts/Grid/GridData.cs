using UnityEngine;

namespace Grid
{
    [System.Serializable]
    public class GridData
    {
        
            public Vector2Int position;
            public tileType tileType;
            public string name;
            public GridData(int x, int y)
            {
                position = new Vector2Int(x, y);
                
            }
        
    }
}