using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    string input = "";
    Vector2Int position;

    void Start() {
        Debug.Log($"Created Player");
        this.position = Map.Instance.getPlayerPosition();
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

    public Vector2Int moveWorker (string instructions){
        
        Vector2Int workerPosition = this.position;

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
            this.checkForWorkerDeath();
        }
        return workerPosition;
    }

    private void checkForWorkerDeath(){
        //TODO: did the worker die?
    }

    public void dig(){
        Debug.Log($"{this}: INSTRUCTIONS TO EXCECUTE: {input}");
        Vector2Int workerPosition = this.moveWorker(input);
        string tileInfo = Map.Instance.getTileInfo(workerPosition);
        Debug.Log($"We found out : {tileInfo}");

        if(tileInfo.Equals("_")){
            Map.Instance.markOnMap(workerPosition, "H");
        }

        if(tileInfo.Equals("D")){
            this.win();
        }
    }


    public void win(){
        Debug.Log($"YOU WIN!!!");
    }



}