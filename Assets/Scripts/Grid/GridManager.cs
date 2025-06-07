using System;
using System.Collections.Generic;
using System.Linq;
using Grid.Tests;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;


namespace Grid
{
    [RequireComponent(typeof(Tilemap))]
    public class GridManager : MonoBehaviour
    {
        // new updates
        public static GridManager Instance;
        
        // creats unitys
        public TileFactory tileFactory;
        public UnitFactory unitFactory;
        
        // the game grid
        public Tilemap tilemap;
        public TilemapCollider2D collider;
        public GameGrid gameGrid;

        // tile dictionaru containes thier data
        private Dictionary<TileBase, TileData> tileTypeMap;
        public List<TileData> listTileData;
        public RuleTile ruleTile;
        
        // unit dictionary
        public UnitList unitList;

        // scriptable object
        public SOGridEvents gridEvents;

        
        
        // current grid
      [SerializeField]  private GridData gridDatashow;
      
      
      
        // enemys to be worked on

        public List<GridData> enemySpawner;

        // pathfinding
        public List<GridData> path;
        public GridData Base;

        private const int MoveStraightCost = 10;
        private const int MoveDiagnolCost = 20;


        
      
        
        

     
   

        private Vector3Int gridposition;
        private void Awake()
        {
            if(Instance == null)
                Instance = this;
            else
            {
                Destroy(gameObject);
            }

        }

        public void Init()
        {
         
            
        }
        

        public void SetUpGrid()
        {
          
        }
        private void Start()
        {
            tilemap = GetComponent<Tilemap>();
            enemySpawner = new List<GridData>();
            
            // set up the grid
            gameGrid = new GameGrid(18, 30,transform.position);
            Debug.Log(collider.bounds.max +" "+collider.bounds.min);
            // setup the grid data
            AssignGridData();
            
            tileFactory = new TileFactory(tilemap,gameGrid);
            unitFactory = new UnitFactory(unitList, gameGrid,tilemap);
            
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
           

            gridposition = tilemap.WorldToCell(position);
            gridDatashow = gameGrid.returnGrid(gridposition.x, gridposition.y);
        }
     

        public void AssignGridData()
        {
            tileTypeMap = new Dictionary<TileBase, TileData>();
            // store the tile and its data in a dictionary for a database
            foreach (var tileData in listTileData)
            {
                foreach (var tile in tileData.tiles)
                {
                    tileTypeMap.Add(tile, tileData);
                }
            }
            // assign each tile in the grid its data
            for (int x = 0; x < gameGrid.width; x++) // Width
            {
                for (int y = 0; y <  gameGrid.length; y++) // Length
                {
                    TileBase tile = tilemap.GetTile(new Vector3Int(x, y, 0));
                    GridData currentData =  gameGrid.returnGrid(x, y);
                    currentData.tileData = tileTypeMap[tile];
                    currentData.name = tile.name;
                    if (currentData.tileData.tileType == tileType.EnemySpawner)
                    {
                        // store the enemy
                        enemySpawner.Add(currentData);
                    }
                    if (currentData.tileData.tileType == tileType.PlayerCore)
                    {
                        Base = currentData;
                    }
                }
            }

         
        }
        public void PlaceTileAtPointer()
        {
            tileFactory.PaintTile(gridposition, tileTypeMap,ruleTile);
        }

        public void PlaceUnitAtPointer(UnitType unitType)
        {
            Vector3 spawnPosition = tilemap.CellToWorld(gridposition);
            unitFactory.SpawnUnit(unitType, spawnPosition);
        }
        public List<GridData> FindPath(Vector3 objectPosition, GridData to)
        {
            Vector3Int gridPosition = tilemap.WorldToCell(objectPosition);
            GridData from = gameGrid.returnGrid(gridPosition.x, gridPosition.y);
            
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

                    if (nieghbor.tileData.tileType == tileType.FertileLand || nieghbor.tileData.tileType == tileType.Barrenland)
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