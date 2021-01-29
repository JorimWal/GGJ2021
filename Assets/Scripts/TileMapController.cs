using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class TileMapController : MonoBehaviour
{
    Tilemap tileMapBehaviour;

    Dictionary<string, Tile> tiles;
    public void Start()
    {
        //Load all necessary resources
        tiles = LoadResources();
        //Retrieve the tilemap component on this object
        tileMapBehaviour = GetComponent<Tilemap>();
        //Clear the tiles on the tilemap
        clearTiles();
        int mapSize = Map.Instance.getMapSize();
        tileMapBehaviour.size = new Vector3Int(mapSize, mapSize, 0);
        
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
        tileMapBehaviour.SetTile(new Vector3Int(coord.x, coord.y, 0), tiles[tile]);
    }

    public void DrawTile(int x, int y, string tile)
    {
        tileMapBehaviour.SetTile(new Vector3Int(x, y, 0), tiles[tile]);
    }

    public void clearTiles(){
        this.tileMapBehaviour.ClearAllTiles();
    }

    public Dictionary<string, Tile> LoadResources()
    {
        Dictionary<string, Tile> output = new Dictionary<string, Tile>();
        Tile[] resources = Resources.LoadAll<Tile>("Tilemap/Tiles");
        foreach(Tile tile in resources)
        {
            output.Add(tile.name, tile);
        }
        return output;
    }
}
