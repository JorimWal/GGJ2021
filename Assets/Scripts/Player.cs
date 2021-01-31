using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    string input = "";
    Vector2Int position;

    public int turnsTaken = 0;
    public int turnslimit = 15;
    public int workersLeft = 5;

    public List<Item> items = new List<Item>();

    public TextBubble textUI;
    public int woodPieces = 0;

    void Start() {
        Debug.Log($"Created Player");
        this.position = Map.Instance.getPlayerPosition();
        UIManager.Instance.setWorkersLeft(this.workersLeft);
        UIManager.Instance.setTurnsTaken(this.turnsTaken, turnslimit);
        UIManager.Instance.setWoodCounter(this.woodPieces);
        textUI.SetContent("Search around the island and Dig at the spot on the treasure map");
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        string oldinput = input;
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            AddInput("U");
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            AddInput("D");
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            AddInput("L");
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            AddInput("R");
        if (Input.GetKeyDown(KeyCode.F))
            AddInput("F");

        if (Input.GetKeyDown(KeyCode.Backspace))
            input = input.Remove(input.Length - 1);

        if(input != oldinput)
        {
            ActionBarController.Instance.ActionInput = input;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            executeInstructions();
            Map.Instance.resetCursor();
        }

        if (Input.GetKeyDown(KeyCode.Escape)){
            input = "";
            ActionBarController.Instance.ActionInput = "";
            Map.Instance.resetCursor();
        }
    }

    public List<Vector2Int> moveWorker (string instructions){
        
        Vector2Int workerPosition = this.position;

        List<Vector2Int> path = new List<Vector2Int>();

        foreach (char instruction in instructions.ToCharArray()) {
            switch(instruction) {
                case 'U':
                    workerPosition = workerPosition + Vector2Int.up;
                    break;
                case 'D':
                    workerPosition = workerPosition + Vector2Int.down;
                    break;
                case 'L':
                    workerPosition = workerPosition + Vector2Int.left;
                    break;
                case 'R':
                    workerPosition = workerPosition + Vector2Int.right;
                    break;
                case 'F':
                    break;
                default:
                    Debug.Log($"Character {instruction} not recognized");
                    break;
            }
            Debug.Log($"Worker moved to {workerPosition}");
            path.Add(workerPosition);
        }
        return path;
    }

    private bool checkForWorkerDeath(List<Vector2Int> path){
        Dictionary<Vector2Int, TileType.TileTypes> grid = Map.Instance.getGrid();
        foreach(Vector2Int coord in path)
        {
            //Nodes outside of map kill workers
            if (!grid.ContainsKey(coord))
                return true;
            //Death nodes kill workers
            Map.Instance.getTileInfo(coord);

            if (grid[coord] == TileType.TileTypes.RIVER){
                
                if(this.woodPieces > 0){
                    Debug.Log($"WORKER USED A WOOD PIECE TO BUILD A BRIDGE");
                    this.woodPieces--;
                    UIManager.Instance.setWoodCounter(this.woodPieces);
                    Map.Instance.buildBridge(coord);
                    return false;
                }
                return true;
            }

        }
        //If no traveled tiles cause death, the worker is not dead
        return false;
    }

    public void executeInstructions(){
        Dictionary<Vector2Int, TileType.TileTypes> grid = Map.Instance.getGrid();

        if (input.Length <= 0)
            return;
        Debug.Log($"{this}: INSTRUCTIONS TO EXCECUTE: {input}");
        List<Vector2Int> path = moveWorker(input);
        //UI color
        Color red = new Color(234f / 255f, 32f / 255f, 39f / 255f);
        if (checkForWorkerDeath(path)) { 
            this.workersLeft--;
            Debug.Log($"{this}: LOST A WORKER: WORKERS LEFT {workersLeft}");

            if(this.doIHaveItem(Item.ItemKind.HOMING_PIDGEON) != null){
                //I have the flying pidgeon so the path is marked anyways.
                for (int i = 0; i < path.Count; i++) {
                    Map.Instance.Reveal(path[i]);
                    if (grid[path[i]] == TileType.TileTypes.RIVER ){
                        break;
                    }
                }
            }

            UIManager.Instance.setWorkersLeft(this.workersLeft);
            if (workersLeft <= 0){
                textUI.SetContent("All your workers have perished, your search is over", red.r, red.g, red.b);
                this.gameOver();
            }
            else
            {
                textUI.SetContent("Your worker did not return from their expedition", red.r, red.g, red.b);
            }
        }
        else
        {
            bool won = false;
            //If the worker does not die
            //Reveal all tiles the worker walked
            for(int i = 0; i < path.Count; i++)
            {
                Map.Instance.Reveal(path[i]);
                //You win if digging on the victory tile
                if (Map.Instance.treasurePosition == path[path.Count - 1] && input[input.Length - 1] == 'F')
                {
                    this.win();
                    won = true;
                }
            }
            //Reveal all tiles directly adjacent to the worker's final spot
            Map.Instance.Reveal(path[path.Count - 1] + Vector2Int.up);
            Map.Instance.Reveal(path[path.Count - 1] + Vector2Int.down);
            Map.Instance.Reveal(path[path.Count - 1] + Vector2Int.left);
            Map.Instance.Reveal(path[path.Count - 1] + Vector2Int.right);
            textUI.SetContent("Your worker brings new insight of your surroundings");
            if(grid[path[path.Count - 1]] == TileType.TileTypes.FOREST){
                Debug.Log($"WOOD GATHERED FROM THIS TILE {path[path.Count - 1]}");
                woodPieces++;
                Map.Instance.chopForest(path[path.Count - 1]);
                UIManager.Instance.setWoodCounter(this.woodPieces);
            } else if(grid[path[path.Count - 1]] == TileType.TileTypes.CHEST) {
                Debug.Log($"ITEM GOT FROM THIS CHEST {path[path.Count - 1]}");
                Item gotItem = Map.Instance.openChest(path[path.Count - 1]);
                Debug.Log($"YOU GOT {gotItem.getName()}");
                this.items.Add(gotItem);
                //UIManager.Instance.setInventory(this.items);
                UIManager.Instance.addAnItemToInventory(gotItem);

            }

           if(!won)
                textUI.SetContent("Your worker brings new insight of your surroundings");
        }
        this.checkForTurns();
        //Empty the action bar for new inputs
        input = "";
        ActionBarController.Instance.ActionInput = "";
    }

    private void checkForTurns(){
        turnsTaken++;
        UIManager.Instance.setTurnsTaken(this.turnsTaken, turnslimit);
        if(turnsTaken > turnslimit){
            this.gameOver();
        }

    }

    public void AddInput(string action) {
        //If the final command is dig, allow no further commands
        if(!(input.Length > 0 && input[input.Length-1] =='F'))
        {
            if(input.Length > 0 && this.oppositeAction(action, input[input.Length - 1].ToString())){
                    Debug.Log($"Actions {action} is opposed to last action {input[input.Length - 1]} so we remove that");
                    this.input = this.input.Substring(0,input.Length - 1);
                    ActionBarController.Instance.ActionInput = this.input;
                    Map.Instance.moveCursorFromPlayer(this.input);
            } else {
                try {
                    this.input += action;
                    ActionBarController.Instance.ActionInput = this.input;
                    Map.Instance.moveCursorFromPlayer(this.input);
                } catch (Exception ex) {
                    Debug.Log("We cant keep going that way:" + ex.ToString());
                    this.input = this.input.Substring(0, input.Length - 1);
                    ActionBarController.Instance.ActionInput = this.input;
                }
                
            }
            
        }
    }
    public void win(){
        Debug.Log($"YOU WIN!!!");
        Color gold = new Color(1, 195f / 255f, 18f / 255f);
        textUI.SetContent("After countless hours of digging, your workers spot a glint of gold. You have found the golden city El Dorado!", gold.r, gold.g, gold.b);
    }

    public void gameOver(){
        Debug.Log($"YOU LOSE!!!");
    }

    public bool oppositeAction(string action, string lastAction){
        switch (action) {
            case "U":
                if (lastAction.Equals("D")) {
                    return true;
                }
                break;
            case "D":
                if (lastAction.Equals("U")) {
                    return true;
                }
                break;
            case "L":
                if (lastAction.Equals("R")) {
                    return true;
                }
                break;
            case "R":
                if (lastAction.Equals("L")) {
                    return true;
                }
                break;
        }
        return false;
    }

    public Item doIHaveItem(Item.ItemKind kind){
        Item item = null;
        foreach (Item itemHold in this.items) {
            if(itemHold.getKind() == kind){
                item = itemHold;
            }
        }
        return item;
    }


}
