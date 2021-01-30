using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BorderType  {

    public enum BorderTypes {
        NORMAL,
        SELECTED
    }
            
    public static string tileName(BorderTypes type) {
        string name = "";
        switch(type){
            case BorderTypes.NORMAL:
                name = "NormalBorder";
                break;
            default:
                name =  "SelectedBorder";
                break;
        }

        return name;
    }



}
