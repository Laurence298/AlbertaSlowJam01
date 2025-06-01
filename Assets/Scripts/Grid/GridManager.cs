using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;


namespace Grid
{
    [RequireComponent(typeof(Tilemap))]
    public class GridManager : MonoBehaviour
    {
        
        public static GridManager Instance;
        
        Tilemap tilemap;
        TilemapCollider2D collider;
        
        public GameGrid gameGrid;
        public GridData gridDatashow;

        public List<TileData> tileDatas = new List<TileData>();
        private Dictionary<TileBase, TileData> dataFromTiles;
        public SOGridEvents gridEvents;

        public List<GridData> enemySpawner;
        public GridData Base;
        public List<GridData> path;


        private const int MoveStraightCost = 10;
        private const int MoveDiagnolCost = 20;
        
        public TileBase highlightTile;

        private Vector3Int gridposition;
        public Vector3 rawPointerposition;
        private void Awake()
        {
            if(Instance == null)
                Instance = this;
            else
            {
                Destroy(gameObject);
            }
            gridEvents.OnMousPointerPositionChanged += GridEventsOnOnMousPointerPositionChanged;
            gridEvents.OnpointerTypeChanged += GridEventsOnOnpointerTypeChanged;
        }
        private void GridEventsOnOnpointerTypeChanged(PointerStates pointerType)
        {
            
        }
        private void GridEventsOnOnMousPointerPositionChanged(Vector3 position)
        {
            if(!gameGrid.IsGridReady())
                return;
            
            rawPointerposition = position;
            gridposition = tilemap.WorldToCell(position);
            gridDatashow = gameGrid.returnGrid(gridposition.x, gridposition.y);
        }
        private void Start()
        {
            tilemap = GetComponent<Tilemap>();
            collider = GetComponent<TilemapCollider2D>();
            enemySpawner = new List<GridData>();
            SetUpGrid();
            AssignGridData();
            /**
            path = FindPath(enemySpawner[0], Base);

            if (path != null)
            {
                Debug.Log(path.Count);
                foreach (GridData data in path)
                {
                    tilemap.SetTile(data.position, highlightTile);
                }
            }
            **/
        }
        public void SetUpGrid()
        {
            gameGrid = new GameGrid(collider.bounds.max, collider.bounds.min,transform.position);
        }
        public void AssignGridData()
        {
            dataFromTiles = new Dictionary<TileBase, TileData>();
            
            
            // store the tile and its data in a dictionary for a database
            foreach (var tileData in tileDatas)
            {
                foreach (var tile in tileData.tiles)
                {
                    dataFromTiles.Add(tile, tileData);
                }
            }
            
            // assign each tile in the grid itsdata
            for (int x = 0; x < gameGrid.width; x++) // Width
            {
                for (int y = 0; y <  gameGrid.length; y++) // Length
                {
                    TileBase tile = tilemap.GetTile(new Vector3Int(x, y, 0));
                    GridData currentData =  gameGrid.returnGrid(x, y);
                   
                    currentData.tileData = dataFromTiles[tile];
                    currentData.name = tile.name;
                    if (currentData.tileData.tileType == tileType.EnemySpawner)
                    {
                        // store the enemy
                        enemySpawner.Add(currentData);
                    }

                    if (currentData.tileData.tileType == tileType.Tower)
                    {
                        Base = currentData;
                    }
                }
            }

         
        }
        public void PaintTile(TileBase tile)
        {
            GridData currentGridPoint = gameGrid.returnGrid(gridposition.x, gridposition.y);

      
            tilemap.SetTile(gridposition, tile);
        }
        public void AssignUnit(GameObject unit)
        { 
            GridData currentGridPoint = gameGrid.returnGrid(gridposition.x, gridposition.y);

          
            Instantiate(unit, tilemap.WorldToCell(rawPointerposition), Quaternion.identity);
        }
        public List<GridData> FindPath(GridData from, GridData to)
        {
            
            
            List<GridData> openPath = new List<GridData>{from};
            List<GridData> closedPath = new List<GridData>();

            for (int x = 0; x < gameGrid.width; x++)
            {
                for (int y = 0; y < gameGrid.length; y++)
                {
                    GridData node = gameGrid.returnGrid(x,y);
                    node.gCost = int.MaxValue;
                    node.CalculateFCosts();
                    node.pathnode = null;
                }
            }

            from.gCost = 0;
            from.hCost = CalculateDistanceCost(from, to);
            Debug.Log(CalculateDistanceCost(from, to));
            from.CalculateFCosts();

            while (openPath.Count > 0)
            {
                GridData currentNode = GetLowestFPathNode(openPath);

                if (currentNode == to)
                {
                    return CalculatePath(to);
                }
                openPath.Remove(currentNode);
                closedPath.Add(currentNode);

                
                foreach (GridData nieghbor in GetNieghbors(currentNode))
                {
                    if(closedPath.Contains(nieghbor) ) continue;

                    if (nieghbor.tileData.tileType == tileType.PlantAble || nieghbor.tileData.tileType == tileType.Empty)
                    {
                        closedPath.Add(nieghbor);
                        continue;
                    }

                    
                    
                    int tetativeGcost = currentNode.gCost + CalculateDistanceCost(currentNode, nieghbor);

                    if (tetativeGcost < nieghbor.gCost)
                    {
                        nieghbor.pathnode = currentNode;
                        nieghbor.gCost = tetativeGcost;
                        nieghbor.hCost = CalculateDistanceCost(currentNode, nieghbor);
                        nieghbor.CalculateFCosts();


                        if (!openPath.Contains(nieghbor))
                        {
                            openPath.Add(nieghbor);
                        }
                    }
                }
            }
            return null;
        }

        public List<GridData> GetNieghbors(GridData currentNode)
        {
            List<GridData> nieghbors = new List<GridData>();

            if (currentNode.position.x - 1 >= 0)
            {
                //left
                nieghbors.Add(gameGrid.returnGrid(currentNode.position.x - 1, currentNode.position.y));
               

            }
            if (currentNode.position.x + 1 < gameGrid.getGrid().GetLength(0))
            {
                //left
                nieghbors.Add(gameGrid.returnGrid(currentNode.position.x + 1, currentNode.position.y));
               

            }
            if(currentNode.position.y -1 >= 0) nieghbors.Add(gameGrid.returnGrid(currentNode.position.x, currentNode.position.y-1));
            if(currentNode.position.y + 1 < gameGrid.length) nieghbors.Add(gameGrid.returnGrid(currentNode.position.x, currentNode.position.y + 1));
            
            return nieghbors;
        }

        List<GridData> CalculatePath(GridData endNode)
        {
            List<GridData> path = new List<GridData>();
            
            path.Add(endNode);
            
            GridData currentNode =endNode;


            while (currentNode.pathnode != null)
            {
                path.Add(currentNode.pathnode);
                currentNode = currentNode.pathnode;
            }
            path.Reverse();
            return path;
        }

        private int CalculateDistanceCost(GridData from, GridData to)
        {
            int xDist = Mathf.Abs(from.position.x - to.position.x);
            int YDist = Mathf.Abs(from.position.y - to.position.y);
            int reamaing = Mathf.Abs(xDist - YDist);
            
            return MoveDiagnolCost + Mathf.Min(xDist, YDist) + MoveStraightCost * reamaing;
        }

        private GridData GetLowestFPathNode(List<GridData> paths)
        {
            GridData lowestPathNode = paths[0];

            for (int i = 0; i < paths.Count; i++)
            {
                if (paths[i].fCost < lowestPathNode.fCost )
                {
                    lowestPathNode = paths[i];
                }
                
            }
            return lowestPathNode;
        }
       
    }
}