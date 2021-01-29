using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    public int mapSize = 10;

    private Dictionary<Vector2Int, string> grid;  

    void Start(){
        this.grid = new Dictionary<Vector2Int, string>();
        this.createMap();
        Debug.Log("Map is " + this.grid);

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

    public string getTileInfo(int x, int y){
        return this.grid[new Vector2Int(x,y)];
    }

}
