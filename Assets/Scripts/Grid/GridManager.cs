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
        public GameObject cellIndicator;
        public Pointer pointer;
        [FormerlySerializedAs("grid")] public GameGrid gameGrid;
        public GridData gridData;
        public Vector3Int startingCellPosition = new Vector3Int(0, 0, 0);

        public List<TileData> tiles = new List<TileData>();
        private Dictionary<TileBase, TileData> dataFromTiles;

        private void Awake()
        {
            
        }

        private void Update()
        {
            Vector2 mouspos = pointer.GetSelectedMapPosition();
            
            if(!gameGrid.IsGridReady())
                return;
            Vector3Int gridposition = tilemap.WorldToCell(mouspos);
            gridData = gameGrid.returnGrid(gridposition.x, gridposition.y);
            Debug.Log(tilemap.GetTile(gridposition)); 
        }

        private void Start()
        {
            tilemap = GetComponent<Tilemap>();
            collider = GetComponent<TilemapCollider2D>();

          
            
       


        }

        public void SetUpGrid()
        {
            gameGrid = new GameGrid(collider.bounds.max, collider.bounds.min,transform.position);
          
            Vector3 startingWorldPosition = tilemap.GetCellCenterWorld(startingCellPosition);
            cellIndicator.transform.position = startingWorldPosition;
        }

        public void AssignGridData()
        {
            dataFromTiles = new Dictionary<TileBase, TileData>();

         
        }
        
       
    }
}