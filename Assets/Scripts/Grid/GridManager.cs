using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;


namespace Grid
{
    [RequireComponent(typeof(Tilemap))]
    public class GridManager : MonoBehaviour
    {
        Tilemap tilemap;
        TilemapCollider2D collider;
        
        public GameGrid gameGrid;
        public GridData gridData;

        public List<TileData> tileDatas = new List<TileData>();
        private Dictionary<TileBase, TileData> dataFromTiles;
        
        public SOGridEvents gridEvents;

        private void Awake()
        {
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
            
            Vector3Int gridposition = tilemap.WorldToCell(position);
            gridData = gameGrid.returnGrid(gridposition.x, gridposition.y);
        }
        private void Start()
        {
            tilemap = GetComponent<Tilemap>();
            collider = GetComponent<TilemapCollider2D>();
            SetUpGrid();
            AssignGridData();
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
            for (int x = 0; x < gameGrid.getGrid().GetLength(0); x++) // Width
            {
                for (int y = 0; y <  gameGrid.getGrid().GetLength(1); y++) // Length
                {
                    TileBase tile = tilemap.GetTile(new Vector3Int(x, y, 0));
                    GridData currentData =  gameGrid.getGrid()[x, y];
                    currentData.tileData = dataFromTiles[tile];
                    currentData.name = tile.name;

                }
            }

         
        }
        public void PaintTile(Vector3Int position, TileBase tile)
        {
            
        }
        public void AssignUnit(GameObject unit, Vector3Int position)
        {
            
        }
        public Vector3Int[] CalculatePath(Vector3Int from, Vector3Int to)
        {
            return new Vector3Int[] { from, to };
        }
       
    }
}