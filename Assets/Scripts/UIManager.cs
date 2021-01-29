using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    private static UIManager _instance;

    public static UIManager Instance { get { return _instance; } }

    public Text workersLeft;
    public Text turnsTaken;


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

    public void setWorkersLeft(int workersLeft){
        this.workersLeft.text = $"Workers x {workersLeft}";
    }

    public void setTurnsTaken(int turnsTaken)
    {
        this.turnsTaken.text = $"Turns x {turnsTaken}";
    }


}
