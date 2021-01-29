using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    void Start(){
        Debug.Log($"Created Player");
    }  

    public void dig(){
        int x = UIManager.Instance.getXValue();
        int y = UIManager.Instance.getYValue();

        Debug.Log($"FOUND: {Map.Instance.getTileInfo(x,y)}");
    }


}