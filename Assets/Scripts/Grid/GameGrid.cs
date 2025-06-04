using System.Collections.Generic;
using UnityEngine;

namespace Grid
{
    [System.Serializable]
    public class GameGrid
    {
    
        private GridData[,] grid;
        private Vector3 originPosition = Vector3.zero;
        private int minLength, maxLength, minWidth, maxWidth;
        public int length, width;
        private bool isready;

        public GameGrid(Vector3 minParam,Vector3 maxparam, Vector3 origin)
        {
           isready = false;
            this.minLength = (int)minParam.y;
            this.maxLength = (int)maxparam.y;
            this.minWidth = (int)minParam.x;
            this.maxWidth =  (int)maxparam.x;
            
            length = Mathf.Abs( ((maxLength - minLength)));
            width =  Mathf.Abs((maxWidth - minWidth ));

            grid = new GridData[width, length];
            
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < length; y++)
                {
                    grid[x, y] = new GridData(x, y);
                }
            }
            isready = true;
        }
        public bool IsGridReady()
        {
            return isready;
        }
        public  GridData returnGrid(int x ,int y)
        {
            int arrayX = x ;
            int arrayY = y ;
            
            // Optional: Safety check
            if (arrayX < 0 || arrayX >width||
                arrayY < 0 || arrayY > length)
            {
                Debug.LogWarning($"Coordinates ({x},{y}) are out of bounds.");
                return null;
            }

            return grid[arrayX, arrayY];
        }
        

        public GridData[,] getGrid()
        {
            return grid;
        }

        public Vector2Int ReturnSize()
        {
            return new Vector2Int(width, length);
        }



    }

   

    public enum tileType
    {
        Barrenland,
        FertileLand,
        EnemyPath,
        EnemySpawner,
        PlayerCore
    }
}