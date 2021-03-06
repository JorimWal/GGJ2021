using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class UIManager : MonoBehaviour {

    private static UIManager _instance;

    public static UIManager Instance { get { return _instance; } }

    public Text workersLeft;
    public Text turnsTaken;
    public Text woodCounter;
    public Tilemap clueTileMap;

    public GameObject actionBar;
    
    public GameObject activeItem;
    public GameObject itemList;

    Dictionary<string, Tile> tiles;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    void Start()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void setWorkersLeft(int workersLeft){
        this.workersLeft.text = $"Workers x {workersLeft}";
    }

    public void setTurnsTaken(int turnsTaken, int turnsLeft)
    {
        this.turnsTaken.text = $"Turns : {turnsTaken} / {turnsLeft}";
    }    
    public void setWoodCounter(int count)
    {
        this.woodCounter.text = $"Wood x {count}";
    }

    public void addAnItemToInventory(Item item){
        GameObject newItem = Instantiate(this.activeItem,Vector3.zero,Quaternion.identity);
        List<Image> images = newItem.GetComponentsInChildren<Image>().ToList();
        //I know this is ugly AF im sorry
        images[1].sprite = Resources.Load<Sprite>(item.getImage());
        newItem.SetActive(true);
        newItem.transform.SetParent(this.itemList.transform);
    }

    public void setClue(Dictionary<Vector2Int, string> clueGrid){
        tiles = TileMapController.LoadResources();
        this.clueTileMap.ClearAllTiles();
        int clueSize = Map.Instance.getClueSizeFromTreasure() + 2;
        this.clueTileMap.size = new Vector3Int(clueSize,clueSize,0);
        Debug.Log($"Setting ClueMap");

        for (int i = 0; i < clueSize; i++)
        {
            for (int j = 0; j < clueSize; j++)
            {
                Vector2Int vector = new Vector2Int(i, j);
                this.clueTileMap.SetTile(new Vector3Int(i, j, 0), tiles[clueGrid[vector]]);
            }
        }
    }
    private Dictionary<string, Tile> LoadResources()
    {
        Dictionary<string, Tile> output = new Dictionary<string, Tile>();
        Tile[] resources = Resources.LoadAll<Tile>("Tilemap/Tiles");
        foreach (Tile tile in resources)
        {
            output.Add(tile.name, tile);
        }
        return output;
    }

}
