﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class TileMapController : MonoBehaviour
{
    Tilemap tileMapBehaviour;

    public Tilemap bordersTileMap;

    Dictionary<string, Tile> tiles;
    Dictionary<string, Tile> borderTiles;
    public void Start()
    {
        //Load all necessary resources
        tiles = LoadResources();
        borderTiles = this.loadBorderTiles();
        //Retrieve the tilemap component on this object
        tileMapBehaviour = GetComponent<Tilemap>();
        //Clear the tiles on the tilemap
        clearTiles();

        int mapSize = Map.Instance.getMapSize();
        //Move the map a little bit so it is centered, while keeping 0,0 at top left
        float halfMapSize = mapSize / 2f;
        transform.position = transform.position - new Vector3(halfMapSize, halfMapSize, 0);
        bordersTileMap.transform.position = bordersTileMap.transform.position - new Vector3(halfMapSize, halfMapSize, 0) + new Vector3(0.5f,-0.5f,0);
        tileMapBehaviour.size = new Vector3Int(mapSize, mapSize, 0);
        bordersTileMap.size = new Vector3Int(mapSize, mapSize, 0);
                
        FillTileMap();
    }

    public void FillTileMap()
    {
        Dictionary<Vector2Int, string> grid = Map.Instance.getGrid();
        int mapSize = Map.Instance.getMapSize();
        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                this.DrawTile(i,j,"QuestionMark");
            }
        }
        DrawTile(Map.Instance.getPlayerPosition(), "P");
    }

    public void DrawTile(Vector2Int coord, string tile)
    {
        this.DrawTile(coord.x, coord.y, tile);
    }

    public void DrawTile(Vector2Int coord, string tile, BorderType.BorderTypes type)
    {
        this.DrawTile(coord.x, coord.y, tile, type);
    }
   
    public void DrawTile(int x, int y, string tile)
    {
        tileMapBehaviour.SetTile(new Vector3Int(x, y, 0), tiles[tile]);
        this.bordersTileMap.SetTile(new Vector3Int(x, y, 0), borderTiles[BorderType.tileName(BorderType.BorderTypes.NORMAL)]);

    }
    public void DrawTile(int x, int y, string tile, BorderType.BorderTypes type)
    {
        tileMapBehaviour.SetTile(new Vector3Int(x, y, 0), tiles[tile]);
        this.bordersTileMap.SetTile(new Vector3Int(x, y, 0), borderTiles[BorderType.tileName(type)]);
    }

    public void changeBorder(Vector2Int coord, BorderType.BorderTypes type){
        this.bordersTileMap.SetTile(new Vector3Int(coord.x, coord.y, 0), borderTiles[BorderType.tileName(type)]);
    }

    public void clearTiles(){
        this.tileMapBehaviour.ClearAllTiles();
    }

    public void resetBorderTiles()
    {
        //This could be way more efficient.
        for (int i = 0; i < Map.Instance.getMapSize(); i++) {
            for (int j = 0; j < Map.Instance.getMapSize(); j++) {
                this.bordersTileMap.SetTile(new Vector3Int(i, j, 0), borderTiles[BorderType.tileName(BorderType.BorderTypes.NORMAL)] );
            }
        }
        
    }

    public static Dictionary<string, Tile> LoadResources()
    {
        Dictionary<string, Tile> output = new Dictionary<string, Tile>();
        Tile[] resources = Resources.LoadAll<Tile>("Tilemap/Tiles");
        foreach(Tile tile in resources)
        {
            output.Add(tile.name, tile);
        }
        return output;
    }

    private Dictionary<string, Tile> loadBorderTiles()
    {
        Dictionary<string, Tile> output = new Dictionary<string, Tile>();
        Tile[] resources = Resources.LoadAll<Tile>("Tilemap/MapBorderTiles");
        foreach (Tile tile in resources)
        {
            output.Add(tile.name, tile);
        }
        return output;
    }

}
