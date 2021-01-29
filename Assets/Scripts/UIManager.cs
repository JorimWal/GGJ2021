using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    private static UIManager _instance;

    public static UIManager Instance { get { return _instance; } }

    public Text workersLeft;


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
         this.workersLeft.text = workersLeft.ToString();
    }


}
