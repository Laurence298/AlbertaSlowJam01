using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Grid.Tests;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Grid
{
    public class UnitFactory
    {
        private UnitList unitList;
        private GameGrid gameGrid;
        private Tilemap tilemap;

        public UnitFactory(UnitList unitList, GameGrid gameGrid, Tilemap tilemap)
        {
            this.unitList = unitList;
            this.gameGrid = gameGrid;
            this.tilemap = tilemap;
        }

        public GameObject SpawnUnit(UnitType type, Vector3 worldPosition)
        {
            if (!unitList.units.ContainsKey(type)) return null;
            
            Vector3Int tilePosition = tilemap.WorldToCell(worldPosition);
            
            if(  gameGrid.returnGrid(tilePosition.x, tilePosition.y).tileData.tileType != tileType.FertileLand   )
                return null;
         
            if(gameGrid.returnGrid(tilePosition.x, tilePosition.y).UnitPlanted)
                return null;
            // update what kind of tile it is
            gameGrid.returnGrid(tilePosition.x, tilePosition.y).UnitPlanted = true;
            
            
            return GameObject.Instantiate(unitList.units[type].unit, new Vector3(worldPosition.x + .5f , worldPosition.y + .5f ,0), Quaternion.identity);
        }
    }

    public enum UnitType
    {
        BaseTree,
        LongRange,
    }
}