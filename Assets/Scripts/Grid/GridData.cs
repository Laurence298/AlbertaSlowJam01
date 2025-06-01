using UnityEngine;

namespace Grid
{
    [System.Serializable]
    public class GridData
    {
        
            public Vector3Int position;
            public TileData tileData;
            public string name;

            public int gCost;
            public int hCost;
            public int fCost;
            public GridData pathnode;
            
            public GridData(int x, int y)
            {
                position = new Vector3Int(x, y);
                
            }

            public void CalculateFCosts()
            {
                fCost = gCost + hCost;
            }
        
    }
}