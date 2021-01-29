using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    private static Map _instance;

    public static Map Instance { get { return _instance; } }

    private Vector2Int playerPosition;

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
        this.grid = new Dictionary<Vector2Int, string>();
        this.createMap();
        this.setFinishTarget();
        this.setPlayer();

    }


    public int mapSize = 10;

    private Dictionary<Vector2Int, string> grid;
    public Vector2Int getPlayerPosition()
    {
        return this.playerPosition;
    }


    void Start(){

    }

    private void createMap(){
        for (int i = 0; i < this.mapSize; i++) {
            for (int j = 0; j < this.mapSize; j++) {
                Vector2Int vector = new Vector2Int(i, j);
                this.grid.Add(vector, "_");
            }
        }
    }
    private void setFinishTarget()
    {
        int x = Random.Range(0,this.mapSize);
        int y = Random.Range(0,this.mapSize);

        this.grid[new Vector2Int(x, y)] = "D";

        Debug.Log($"treasure is in {x},{y}: {this.getTileInfo(x,y)}");

    }

    private void setPlayer()
    {
        int x = Random.Range(0, this.mapSize);
        int y = Random.Range(0, this.mapSize);

        Vector2Int playerPosition = new Vector2Int(x, y);

        this.grid[playerPosition] = "P";

        this.playerPosition = playerPosition;

        Debug.Log($"Player is in {x},{y}: {this.getTileInfo(x, y)}");

    }


    public Dictionary<Vector2Int, string> getGrid(){
        return this.grid;
    }

    public string getTileInfo(int x, int y){
        return this.grid[new Vector2Int(x,y)];
    }

    public string getTileInfo(Vector2Int position)
    {
        return this.grid[position];
    }


    public void populateMap(){

    }
    public int getMapSize()
    {
        return this.mapSize;
    }

    public void setMapSize(int mapSize)
    {
        this.mapSize = mapSize;
    }



}
