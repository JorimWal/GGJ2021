using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    string input = "";
    Vector2Int position;

    public int turnsTaken = 0;
    public int workersLeft = 5;

    void Start() {
        Debug.Log($"Created Player");
        this.position = Map.Instance.getPlayerPosition();
        UIManager.Instance.setWorkersLeft(this.workersLeft);
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        string oldinput = input;
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            input += "U";
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            input += "D";
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            input += "L";
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            input += "R";

        if (Input.GetKeyDown(KeyCode.Backspace))
            input = input.Remove(input.Length - 1);

        if(input != oldinput)
        {
            ActionBarController.Instance.ActionInput = input;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            dig();
            input = "";
            ActionBarController.Instance.ActionInput = "";
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
        Dictionary<Vector2Int, string> grid = Map.Instance.getGrid();
        foreach(Vector2Int coord in path)
        {
            //Nodes outside of map kill workers
            if (!grid.ContainsKey(coord))
                return true;
            //Death nodes kill workers
            if (grid[coord] == "D")
                return true;
        }
        //If no traveled tiles cause death, the worker is not dead
        return false;
    }

    public void dig(){
        turnsTaken++;
        Debug.Log($"{this}: INSTRUCTIONS TO EXCECUTE: {input}");
        List<Vector2Int> path = moveWorker(input);
        Vector2Int workerPosition = path[path.Count - 1];
        if (checkForWorkerDeath(path)) { 
            this.workersLeft--;
            Debug.Log($"{this}: LOST A WORKER: WORKERS LEFT {workersLeft}");
            UIManager.Instance.setWorkersLeft(this.workersLeft);
            if (workersLeft <= 0){
                this.gameOver();
            }
        }
        else
        {
            //If the worker does not die
            //Reveal all tiles the worker walked
            for(int i = 0; i < path.Count; i++)
            {
                Map.Instance.Reveal(path[i]);
                if (Map.Instance.getTileInfo(path[i]) == "T")
                    this.win();
            }
            //Reveal all tiles directly adjacent to the worker's final spot
            Map.Instance.Reveal(path[path.Count - 1] + Vector2Int.up);
            Map.Instance.Reveal(path[path.Count - 1] + Vector2Int.down);
            Map.Instance.Reveal(path[path.Count - 1] + Vector2Int.left);
            Map.Instance.Reveal(path[path.Count - 1] + Vector2Int.right);
        }
    }


    public void win(){
        Debug.Log($"YOU WIN!!!");
    }

    public void gameOver(){
        Debug.Log($"YOU LOSE!!!");
    }


}