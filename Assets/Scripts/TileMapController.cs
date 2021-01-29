using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class TileMapController : MonoBehaviour
{
    Tilemap tileMapBehaviour;

    [Tooltip("Types of tiles")]
    public List<Tile> Tiles;
    [Tooltip("String value of tiles")]
    public List<string> TileValues;
    public void Start()
    {
        //Retrieve the tilemap component on this object
        tileMapBehaviour = GetComponent<Tilemap>();
        this.clearTiles();
        int mapSize = Map.Instance.getMapSize();
        tileMapBehaviour.size = new Vector3Int(mapSize, mapSize, 0);
        // //TEST: fill tilemap with first element
        // Dictionary<Vector2Int, string> testDict = new Dictionary<Vector2Int, string>();
        // testDict.Add(new Vector2Int(0, 0), "H");
        
        this.FillTileMap();
    }

    public void FillTileMap()
    {
        Dictionary<Vector2Int, string> grid = Map.Instance.getGrid();
        int mapSize = Map.Instance.getMapSize();
        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                string info = Map.Instance.getTileInfo(i,j);
                this.DrawTile(i,j,info);
            }
        }

    }

    public void DrawTile(Vector2Int coord, string tile)
    {
        tileMapBehaviour.SetTile(new Vector3Int(coord.x, coord.y, 0), Tiles[TileValues.IndexOf(tile)]);
    }

    public void DrawTile(int x, int y, string tile)
    {
        tileMapBehaviour.SetTile(new Vector3Int(x, y, 0), Tiles[TileValues.IndexOf(tile)]);
    }

    public void clearTiles(){
        this.tileMapBehaviour.ClearAllTiles();
    }
}
