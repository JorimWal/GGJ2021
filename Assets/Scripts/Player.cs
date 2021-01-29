using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    Vector2Int position;

    void Start(){
        Debug.Log($"Created Player");
        this.position = Map.Instance.getPlayerPosition();
    }  


    public void executeInstructions (string instructions){
        
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
        Debug.Log($"We found out : {Map.Instance.getTileInfo(workerPosition)}");

    }

    private void checkForWorkerDeath(){
        //TODO: did the worker die?
    }

    public void dig(){
        string instructions = UIManager.Instance.getInstructions();
        Debug.Log($"{this}: INSTRUCTIONS TO EXCECUTE: {instructions}");
        this.executeInstructions(instructions);
    }




}