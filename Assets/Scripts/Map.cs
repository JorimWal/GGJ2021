using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Map : MonoBehaviour {
    
    public int mapSize = 10;
    public int forestTiles = 7;

    public int desertTiles = 3;
    public int riverTiles = 5;
    public int mountainTiles = 4;

    public int clueSizeFromTreasure = 1;

    public const int MAX_TRIES_FOR_MAP_GENERATION = 5;

    private static Map _instance;

    public static Map Instance { get { return _instance; } }

    private Vector2Int playerPosition;
    private Vector2Int cursorPosition;
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
        this.grid = new Dictionary<Vector2Int, TileType.TileTypes>();
        this.createMap();
    }


    public int getClueSizeFromTreasure()
    {
        return this.clueSizeFromTreasure;
    }

    private Dictionary<Vector2Int, TileType.TileTypes> grid;
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
                this.grid.Add(vector, TileType.TileTypes.NORMAL);
            }
        }
        
        setFinishTarget();
        setPlayer();
        for (int i = 0; i < this.forestTiles; i++)
        {
            putForestInTheMap();
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

        this.grid[playerPosition] = TileType.TileTypes.PLAYER;

        this.playerPosition = playerPosition;
        this.cursorPosition = playerPosition;

        Debug.Log($"Player is in {playerPosition}: {this.getTileInfo(playerPosition)}");
    }    
    
    private void putForestInTheMap(){
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

        this.grid[position] = TileType.TileTypes.FOREST;
        Debug.Log($"Forest added in {position}");

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

        this.grid[position] = TileType.TileTypes.DESERT;
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

        this.grid[position] = TileType.TileTypes.RIVER;
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

        this.grid[position] = TileType.TileTypes.MOUNTAIN;
        Debug.Log($"Mountain added in {position}");

    }

    public void buildBridge(Vector2Int position){
        this.grid[position] = TileType.TileTypes.BRIDGE;
        tileMapController.DrawTile(position, TileType.TileTypes.BRIDGE);
    }


    public Dictionary<Vector2Int, TileType.TileTypes> getGrid(){
        return this.grid;
    }

    public TileType.TileTypes getTileInfo(int x, int y){
        return this.grid[new Vector2Int(x,y)];
    }

    public TileType.TileTypes getTileInfo(Vector2Int position)
    {
        try{
            return this.grid[position];
        } catch(KeyNotFoundException ex) {
            Debug.Log($"Position {position} is out of the limits for this grid. Returning X");
            return TileType.TileTypes.NOTHING; 
        }
    }

    public TileType.TileTypes getTileInfoCountForHidden(Vector2Int position)
    {
        try
        {
            string info = this.tileMapController.GetTileInfo(position);
            if (info.Equals("QuestionMark")){
                return TileType.TileTypes.QUESTION_MARK;
            }
            return this.grid[position];
        }
        catch (KeyNotFoundException ex)
        {
            Debug.Log($"Position {position} is out of the limits for this grid. Returning X");
            return TileType.TileTypes.NOTHING;
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
        tileMapController.DrawTile(position, TileType.TileTypes.NORMAL);
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
        tileMapController.DrawTile(position, TileType.TileTypes.TREASURE);
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
        return (this.treasurePosition != possibleDesertPosition && this.getTileInfo(possibleDesertPosition) == TileType.TileTypes.NORMAL);
    }
    private bool isRiverTheoricalSpawnPositionCorrect(Vector2Int possibleRiverPosition) {
        return (this.treasurePosition != possibleRiverPosition && this.getTileInfo(possibleRiverPosition) == TileType.TileTypes.NORMAL);
    }
    private bool isMountainTheoricalMountainPositionCorrect(Vector2Int possibleMountainPosition) {
        return (this.treasurePosition != possibleMountainPosition && this.getTileInfo(possibleMountainPosition) == TileType.TileTypes.NORMAL);
    }

    public void moveCursorFromPlayer(string input){
        
        this.resetCursor();
        BorderType.BorderTypes borderWeAreUsing = BorderType.BorderTypes.SELECTED;

        foreach (char instruction in input.ToCharArray())
        {
            switch (instruction)
            {
                case 'U':
                    this.cursorPosition = this.cursorPosition + Vector2Int.up;
                    break;
                case 'D':
                    this.cursorPosition = this.cursorPosition + Vector2Int.down;
                    break;
                case 'L':
                    this.cursorPosition = this.cursorPosition + Vector2Int.left;
                    break;
                case 'R':
                    this.cursorPosition = this.cursorPosition + Vector2Int.right;
                    break;
                case 'F':
                    break;
                default:
                    Debug.Log($"Character {instruction} not recognized");
                    break;
            }
            if(this.getTileInfoCountForHidden(this.cursorPosition) == TileType.TileTypes.DESERT ){
                borderWeAreUsing = BorderType.BorderTypes.DANGER;
            };
            Debug.Log($"We draw the cursor over {this.cursorPosition}");
            this.tileMapController.changeBorder(this.cursorPosition, borderWeAreUsing);
        }

    }

    public void resetCursor(){
        this.cursorPosition = this.playerPosition;
        this.tileMapController.resetBorderTiles();
    }

    public void setClue(){
        Dictionary<Vector2Int, string> clueGrid = new Dictionary<Vector2Int, string>();
        
        Vector2Int position = this.treasurePosition;

        for (int i = -this.clueSizeFromTreasure; i <= this.clueSizeFromTreasure; i++)
        {
            for (int j = -this.clueSizeFromTreasure; j <= this.clueSizeFromTreasure; j++) {

                Vector2Int vector = this.treasurePosition + new Vector2Int(i, j);

                TileType.TileTypes info = getTileInfo(vector);
                if (i == 0 && j == 0)
                {
                    info = TileType.TileTypes.TREASURE;
                }
                
                clueGrid.Add(new Vector2Int(i + this.clueSizeFromTreasure,j + this.clueSizeFromTreasure), TileType.tileName(info));
            }
        }

        UIManager.Instance.setClue(clueGrid);
    }



}
