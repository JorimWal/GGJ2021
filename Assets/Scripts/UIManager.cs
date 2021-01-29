using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    private static UIManager _instance;

    public static UIManager Instance { get { return _instance; } }

    public InputField xInput;
    public InputField yInput;


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

    public int getXValue(){
        return int.Parse(this.xInput.text);
    }
    public int getYValue(){
        return int.Parse(this.yInput.text);
    }


}
