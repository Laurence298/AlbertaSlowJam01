﻿using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Grid
{
    public class TileFactory
    {
        private Tilemap tilemap;
        private GameGrid grid ;
         

        public TileFactory(Tilemap tilemap, GameGrid grid)
        {
            this.grid = grid;
            this.tilemap = tilemap;
        }

        public void PaintTile(Vector3Int position,Dictionary<TileBase, TileData> tileTypeMap, RuleTile ruleTile)
        {
            if(  grid.returnGrid(position.x, position.y).tileData.tileType != tileType.Barrenland)
                return;
            
            tilemap.SetTile(position, ruleTile);
            TileBase newTile = tilemap.GetTile(position);
            grid.returnGrid(position.x, position.y).tileData =tileTypeMap[newTile]; ;
           
        }
    }
}