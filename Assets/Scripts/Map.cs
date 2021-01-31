using System.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Map : MonoBehaviour {
    
    public int mapSize = 10;
    public int forestTiles = 7;
    public int chestsTiles = 1;
    public int desertTiles = 3;
    public int riverTiles = 5;
    public int mountainTiles = 4;

    public int clueSizeFromTreasure = 1;

    public const int MAX_TRIES_FOR_MAP_GENERATION = 5;

    private static Map _instance;

    public MapType map = MapType.Random;

    public static Map Instance { get { return _instance; } }

    private Vector2Int playerPosition;
    private Vector2Int cursorPosition;
    public Vector2Int treasurePosition;

    private List<Vector2Int> dangerPositions;

    public TileMapController tileMapController;

    private UIManager uIManager;

    public TextBubble textBubble;

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
        if(this.grid == null) {
            this.grid = new Dictionary<Vector2Int, TileType.TileTypes>();
            this.uIManager = GetComponent<UIManager>();
            this.createMap(map);
        }
    }


    public int getClueSizeFromTreasure()
    {
        return this.clueSizeFromTreasure;
    }

    public void setClueSizeFromTreasure(int clueSizeFromTreasure)
    {
        this.clueSizeFromTreasure = clueSizeFromTreasure;
    }

    private Dictionary<Vector2Int, TileType.TileTypes> grid;
    public Vector2Int getPlayerPosition()
    {
        return this.playerPosition;
    }


    void Start(){
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        if (this.grid == null)
        {
            this.grid = new Dictionary<Vector2Int, TileType.TileTypes>();
            this.createMap(map);
        }
    }

    public enum MapType { Random, Tutorial1, Tutorial2}

    private void createMap(MapType mapType = MapType.Random) {
        switch (mapType)
        {
            case MapType.Tutorial1:
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        Vector2Int vector = new Vector2Int(i, j);
                        this.grid.Add(vector, TileType.TileTypes.NORMAL);
                    }
                }
                grid[new Vector2Int(0, 0)] = TileType.TileTypes.PLAYER;
                grid[new Vector2Int(5, 9)] = TileType.TileTypes.FOREST;
                grid[new Vector2Int(5, 8)] = TileType.TileTypes.TREASURE;
                this.treasurePosition = new Vector2Int(5, 8);
                textBubble.SetContent("Captain's Log, day 29. It has been near a month since we set out", queueMessage: true);
                textBubble.SetContent("to find the City of Gold that the locals assured us could not be found.", queueMessage: true);
                textBubble.SetContent("Ofcourse something as trivial as 'being made up entirely by you' isn't", queueMessage: true);
                textBubble.SetContent("going to stop a grand explorer, such as myself! I have been told that", queueMessage: true);
                textBubble.SetContent("the first clue to El Dorado is buried south of the great lonesome Forest.", queueMessage: true);
                textBubble.SetContent("Honestly with a charting system like this no wonder they never found the damn place", queueMessage: true);
                textBubble.SetContent("I should send some men out to go look for this tree. And instruct them to dig", queueMessage: true);
                textBubble.SetContent("south of the Forest. P.S. I should remember to give the directions", queueMessage : true);
                textBubble.SetContent("step by step, these men get lost so easily.", queueMessage : true);

                setClue();
                break;
            case MapType.Tutorial2:
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        Vector2Int vector = new Vector2Int(i, j);
                        this.grid.Add(vector, TileType.TileTypes.NORMAL);
                    }
                }
                grid[new Vector2Int(0, 0)] = TileType.TileTypes.PLAYER;
                grid[new Vector2Int(0, 1)] = TileType.TileTypes.CHEST;
                //Generate a 3x3 forest on the right side of the map
                grid[new Vector2Int(9, 0)] = TileType.TileTypes.FOREST;
                grid[new Vector2Int(9, 1)] = TileType.TileTypes.FOREST;
                grid[new Vector2Int(9, 2)] = TileType.TileTypes.FOREST;
                grid[new Vector2Int(8, 0)] = TileType.TileTypes.FOREST;
                grid[new Vector2Int(8, 1)] = TileType.TileTypes.FOREST;
                grid[new Vector2Int(8, 2)] = TileType.TileTypes.FOREST;
                grid[new Vector2Int(7, 0)] = TileType.TileTypes.FOREST;
                grid[new Vector2Int(7, 1)] = TileType.TileTypes.FOREST;
                grid[new Vector2Int(7, 2)] = TileType.TileTypes.FOREST;
                grid[new Vector2Int(0, 9)] = TileType.TileTypes.MOUNTAIN;
                for(int x = 0; x < 10; x++)
                {
                    grid[new Vector2Int(x, 5)] = TileType.TileTypes.RIVER;
                }
                this.treasurePosition = new Vector2Int(0, 8);
                textBubble.SetContent("Captain's Log, day 96. I am once again hopelessly lost.", queueMessage: true);
                textBubble.SetContent("The last clue we had rumored that the Dorado we hope is El Dorado ", queueMessage: true);
                textBubble.SetContent("Should be to the North and right at the cusp of the mountains.", queueMessage: true);
                textBubble.SetContent("I should send more lacke- I mean workers out to find these mountains straight away.", queueMessage: true);
                setClue();
                break;
            default:
                for (int i = 0; i < mapSize; i++)
                {
                    for (int j = 0; j < mapSize; j++)
                    {
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
                for (int i = 0; i < this.desertTiles; i++)
                {
                    putDesertInTheMap();
                }
                for (int i = 0; i < this.riverTiles; i++)
                {
                    putRiverInTheMap();
                }
                for (int i = 0; i < this.mountainTiles; i++)
                {
                    putMountainInTheMap();
                }
                for (int i = 0; i < this.chestsTiles; i++)
                {
                    putChestInTheMap();
                }

                textBubble.SetContent("Search around the island and Dig at the spot on the treasure map");
                setClue();
                break;
        }

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
    private void putChestInTheMap(){
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

        } while (!this.isChestTheoricalMountainPositionCorrect(position));

        this.grid[position] = TileType.TileTypes.CHEST;
        Debug.Log($"Chest added in {position}");

    }

    public void buildBridge(Vector2Int position){
        this.grid[position] = TileType.TileTypes.BRIDGE;
        tileMapController.DrawTile(position, TileType.TileTypes.BRIDGE);
    }

    public void chopForest(Vector2Int position){
        this.grid[position] = TileType.TileTypes.CHOPPED_FOREST;
        tileMapController.DrawTile(position, TileType.TileTypes.CHOPPED_FOREST);
    }
    public Item openChest(Vector2Int position){
        this.grid[position] = TileType.TileTypes.OPEN_CHEST;
        tileMapController.DrawTile(position, TileType.TileTypes.OPEN_CHEST);
        return Item.createItem(Item.ItemKind.ANCESTRAL_KNOWLEDGE);
    }

    public void wrongDigSite(Vector2Int position){
        this.grid[position] = TileType.TileTypes.DIG_ATTEMPT;
        tileMapController.DrawTile(position, TileType.TileTypes.DIG_ATTEMPT);
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
    private bool isChestTheoricalMountainPositionCorrect(Vector2Int possibleMountainPosition) {
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

            
            TileType.TileTypes type = this.getTileInfoCountForHidden(this.cursorPosition);

            if (type == TileType.TileTypes.NOTHING) {
                Debug.Log($"Player is out of bounds! Lets go back");
                throw new Exception("Grid out of bounds!");
            }

            if(type == TileType.TileTypes.RIVER ){
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

        this.uIManager.setClue(clueGrid);
    }
}
