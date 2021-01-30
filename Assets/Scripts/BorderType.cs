using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BorderType  {

    public enum BorderTypes {
        NORMAL,
        SELECTED,
        PLAYER,
        DANGER
    }
            
    public static string tileName(BorderTypes type) {
        string name = "";
        switch(type){
            case BorderTypes.NORMAL:
                name = "NormalBorder";
                break;
            case BorderTypes.SELECTED:
                name = "SelectedBorder";
                break;
            case BorderTypes.PLAYER:
                name = "PlayerBorder";
                break;
            case BorderTypes.DANGER:
                name = "DangerBorder";
                break;
            default:
                name =  "NormalBorder";
                break;
        }

        return name;
    }



}
