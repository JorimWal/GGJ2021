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

        //TEST: fill tilemap with first element
        Dictionary<Vector2Int, string> testDict = new Dictionary<Vector2Int, string>();
        testDict.Add(new Vector2Int(0, 0), "H");
        FillTileMap(testDict);
    }

    public void FillTileMap(Dictionary<Vector2Int, string> mapDictionary)
    {
        foreach(var item in mapDictionary)
        {
            DrawTile(item.Key, item.Value);
        }
    }

    public void DrawTile(Vector2Int coord, string tile)
    {
        tileMapBehaviour.SetTile(new Vector3Int(coord.x, coord.y, 0), Tiles[TileValues.IndexOf(tile)]);
    }
}
