using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    private static Map _instance;

    public static Map Instance { get { return _instance; } }


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


    public int mapSize = 10;

    private Dictionary<Vector2Int, string> grid;  

    void Start(){
        this.grid = new Dictionary<Vector2Int, string>();
        this.createMap();
        Debug.Log("Map is " + this.grid);
        this.setFinishTarget();
        string tileInfo = this.getTileInfo(1,2);
        Debug.Log("Tile info is " + tileInfo);
    }

    private void createMap(){
        for (int i = 0; i < this.mapSize; i++) {
            for (int j = 0; j < this.mapSize; j++) {
                Vector2Int vector = new Vector2Int(i, j);
                this.grid.Add(vector, "a");
            }
        }
    }
    private void setFinishTarget()
    {
        int x = Random.Range(0,this.mapSize);
        int y = Random.Range(0,this.mapSize);

        this.grid[new Vector2Int(x, y)] = "T";

        Debug.Log($"treasure is in {x},{y}: {this.getTileInfo(x,y)}");


    }

    public Dictionary<Vector2Int, string> getGrid(){
        return this.grid;
    }

    public string getTileInfo(int x, int y){
        return this.grid[new Vector2Int(x,y)];
    }



}
