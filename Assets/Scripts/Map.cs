using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Map : MonoBehaviour {


    public int mapSize = 10;

    public int deathTiles = 5;

    public int desertTiles = 7;
    public int riverTiles = 5;
    public int mountainTiles = 4;

    public int clueSizeFromTreasure = 1;

    public const int MAX_TRIES_FOR_MAP_GENERATION = 5;

    private static Map _instance;

    public static Map Instance { get { return _instance; } }

    private Vector2Int playerPosition;
    public Vector2Int treasurePosition;

    private List<Vector2Int> dangerPositions;

    public TileMapController tileMapController;

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
    }


    public int getClueSizeFromTreasure()
    {
        return this.clueSizeFromTreasure;
    }

    private Dictionary<Vector2Int, string> grid;
    public Vector2Int getPlayerPosition()
    {
        return this.playerPosition;
    }


    void Start(){

    }

    private void createMap(){
       
        
        for (int i = 0; i < mapSize; i++) {
            for (int j = 0; j < mapSize; j++) {
                Vector2Int vector = new Vector2Int(i, j);
                this.grid.Add(vector, "_");
            }
        }
        
        setFinishTarget();
        setPlayer();
        for (int i = 0; i < this.deathTiles; i++) {
            putDangerInTheMap();
        }
        for (int i = 0; i < this.desertTiles; i++) {
            putDesertInTheMap();
        }
        for (int i = 0; i < this.riverTiles; i++) {
            putRiverInTheMap();
        }
        for (int i = 0; i < this.mountainTiles; i++) {
            putMountainInTheMap();
        }


        setClue();
    }
    private void setFinishTarget()
    {
        int x = UnityEngine.Random.Range(0, mapSize);
        int y = UnityEngine.Random.Range(0, mapSize);

        Vector2Int position = new Vector2Int(x, y);

        this.treasurePosition = position;

        Debug.Log($"treasure is in {x},{y}: {this.getTileInfo(x,y)}");

    }


    private void setPlayer()
    {
        int tries = 0;
        Vector2Int playerPosition;
        do {
            int x = UnityEngine.Random.Range(0, mapSize);
            int y = UnityEngine.Random.Range(0, mapSize);
            playerPosition = new Vector2Int(x, y);
            tries++;

            if(tries > MAX_TRIES_FOR_MAP_GENERATION){
                throw new System.Exception("MAX NUMBER OF MAP GENERATION TRIES REACHED");
            }

        } while(!this.isPlayerTheoricalSpawnPositionCorrect(playerPosition));

        this.grid[playerPosition] = "P";

        this.playerPosition = playerPosition;

        Debug.Log($"Player is in {playerPosition}: {this.getTileInfo(playerPosition)}");
    }    
    
    private void putDangerInTheMap()
    {
        int tries = 0;
        Vector2Int dangerPosition;
        this.dangerPositions = new List<Vector2Int>();
        do {
            int x = UnityEngine.Random.Range(0, mapSize);
            int y = UnityEngine.Random.Range(0, mapSize);
            dangerPosition = new Vector2Int(x, y);
            tries++;

            if(tries > MAX_TRIES_FOR_MAP_GENERATION ){
                throw new System.Exception("MAX NUMBER OF MAP GENERATION TRIES REACHED");
            }

        } while(!this.isDangerTheoricalSpawnPositionCorrect(dangerPosition));

        this.grid[dangerPosition] = "D";

        this.dangerPositions.Add(playerPosition);

        Debug.Log($"Dangers are in {this.dangerPositions}");

    }

    private void putDesertInTheMap(){
        int tries = 0;
        Vector2Int position;
        do
        {
            int x = UnityEngine.Random.Range(0, mapSize);
            int y = UnityEngine.Random.Range(0, mapSize);
            position = new Vector2Int(x, y);
            tries++;

            if (tries > MAX_TRIES_FOR_MAP_GENERATION)
            {
                throw new System.Exception("MAX NUMBER OF MAP GENERATION TRIES REACHED");
            }

        } while (!this.isDesertTheoricalSpawnPositionCorrect(position));

        this.grid[position] = "L";
        Debug.Log($"Desert added in {position}");

    }   
    private void putRiverInTheMap(){
        int tries = 0;
        Vector2Int position;
        do
        {
            int x = UnityEngine.Random.Range(0, mapSize);
            int y = UnityEngine.Random.Range(0, mapSize);
            position = new Vector2Int(x, y);
            tries++;

            if (tries > MAX_TRIES_FOR_MAP_GENERATION)
            {
                throw new System.Exception("MAX NUMBER OF MAP GENERATION TRIES REACHED");
            }

        } while (!this.isRiverTheoricalSpawnPositionCorrect(position));

        this.grid[position] = "S";
        Debug.Log($"River added in {position}");

    }
    private void putMountainInTheMap(){
        int tries = 0;
        Vector2Int position;
        do
        {
            int x = UnityEngine.Random.Range(0, mapSize);
            int y = UnityEngine.Random.Range(0, mapSize);
            position = new Vector2Int(x, y);
            tries++;

            if (tries > MAX_TRIES_FOR_MAP_GENERATION)
            {
                throw new System.Exception("MAX NUMBER OF MAP GENERATION TRIES REACHED");
            }

        } while (!this.isMountainTheoricalMountainPositionCorrect(position));

        this.grid[position] = "M";
        Debug.Log($"Mountain added in {position}");

    }


    public Dictionary<Vector2Int, string> getGrid(){
        return this.grid;
    }

    public string getTileInfo(int x, int y){
        return this.grid[new Vector2Int(x,y)];
    }

    public string getTileInfo(Vector2Int position)
    {
        try{
            return this.grid[position];
        } catch(KeyNotFoundException ex) {
            Debug.Log($"Position {position} is out of the limits for this grid. Returning X");
            return "X"; 
        }
    }

    public void Reveal(Vector2Int position)
    {
        if (!grid.ContainsKey(position))
            return;
        tileMapController.DrawTile(position, grid[position]);
    }

    public void Hide(Vector2Int position)
    {
        if (!grid.ContainsKey(position))
            return;
        tileMapController.DrawTile(position, "_");
    }

    public int getMapSize()
    {
        return mapSize;
    }

    public void setMapSize(int mapSize)
    {
        this.mapSize = mapSize;
    }

    public void revealAll(){
        for (int i = 0; i < mapSize; i++) {
            for (int j = 0; j < mapSize; j++) {
                Vector2Int vector = new Vector2Int(i,j);
                tileMapController.DrawTile(vector, grid[vector]);
            }
        }
    }

    public void revealTreasure(){
        Vector2Int position = this.treasurePosition;
        tileMapController.DrawTile(position, "T");
    }

    private bool isPlayerTheoricalSpawnPositionCorrect(Vector2Int possiblePlayerPosition){

        Debug.Log($"Treasure is im {treasurePosition}");
        Debug.Log($"Is Player able to spawn in {possiblePlayerPosition}");
        float diff = Mathf.Abs(treasurePosition.sqrMagnitude - possiblePlayerPosition.sqrMagnitude);
        Debug.Log($"SQR MAGNITUDE DIFF IS {diff.ToString()}"); 
        if (diff < 16) {
            Debug.Log($"PLAYER IS TOO CLOSE TO TREASURE {diff.ToString()}");
            return false;
        }
        return true;

    }
    private bool isDangerTheoricalSpawnPositionCorrect(Vector2Int possibleDangerPosition){

        float diff = 99;
        foreach (Vector2Int dangerPosition in this.dangerPositions) {
            diff = Mathf.Abs(dangerPosition.sqrMagnitude - possibleDangerPosition.sqrMagnitude);
            if (diff < 5) {
                Debug.Log($"DANGER IS TOO CLOSE TO OTHER DANGER {diff.ToString()}");    
                return false;
            }
        }
        Debug.Log($"Treasure is in {this.treasurePosition}");
        diff = Mathf.Abs(this.treasurePosition.sqrMagnitude - possibleDangerPosition.sqrMagnitude);
        Debug.Log($"SQR MAGNITUDE DIFF IS {diff.ToString()}");
        if (diff < 5)
        {
            Debug.Log($"DANGER IS TOO CLOSE TO TREASURE {diff.ToString()}");
            return false;
        }

        Debug.Log($"Player is in {this.playerPosition}");
        diff = Mathf.Abs(this.playerPosition.sqrMagnitude - possibleDangerPosition.sqrMagnitude);
        Debug.Log($"SQR MAGNITUDE DIFF IS {diff.ToString()}"); 
        if (diff < 16) {
            Debug.Log($"DANGER IS TOO CLOSE TO PLAYER {diff.ToString()}");
            return false;
        }
        return true;
    }

    private bool isDesertTheoricalSpawnPositionCorrect(Vector2Int possibleDesertPosition) {
        return this.getTileInfo(possibleDesertPosition) == "_";
    }
    private bool isRiverTheoricalSpawnPositionCorrect(Vector2Int possibleRiverPosition) {
        return this.getTileInfo(possibleRiverPosition) == "_";
    }
    private bool isMountainTheoricalMountainPositionCorrect(Vector2Int possibleMountainPosition) {
        return this.getTileInfo(possibleMountainPosition) == "_";
    }


    public void setClue(){
        Dictionary<Vector2Int, string> clueGrid = new Dictionary<Vector2Int, string>();
        
        Vector2Int position = this.treasurePosition;

        for (int i = -this.clueSizeFromTreasure; i <= this.clueSizeFromTreasure; i++)
        {
            for (int j = -this.clueSizeFromTreasure; j <= this.clueSizeFromTreasure; j++) {

                Vector2Int vector = this.treasurePosition + new Vector2Int(i, j);
                
                string info = getTileInfo(vector);
                if (i == 0 && j == 0)
                {
                    info = "T";
                }
                
                clueGrid.Add(new Vector2Int(i + this.clueSizeFromTreasure,j + this.clueSizeFromTreasure),info);
            }
        }

        UIManager.Instance.setClue(clueGrid);
    }



}
