using System.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TileType  {

    public enum TileTypes {
        NORMAL,
        FOREST,
        RIVER,
        BRIDGE,
        MOUNTAIN,
        DESERT,
        PLAYER,
        QUESTION_MARK,
        UNDERSCORE,
        NOTHING,
        TREASURE
    }
            
    public static string tileName(TileTypes type) {
        string name = "";
        switch(type){
            case TileTypes.NORMAL:
            case TileTypes.UNDERSCORE:
                name = "_";
                break;
            case TileTypes.FOREST:
                name = "F";
                break;
            case TileTypes.PLAYER:
                name = "P";
                break;
            case TileTypes.RIVER:
                name = "R";
                break;
            case TileTypes.MOUNTAIN:
                name = "M";
                break;
            case TileTypes.DESERT:
                name = "D";
                break;
            case TileTypes.TREASURE:
                name = "T";
                break;
            case TileTypes.BRIDGE:
                name = "B";
                break;
            case TileTypes.QUESTION_MARK:
                name = "QuestionMark";
                break;
            case TileTypes.NOTHING:
                name = "X";
                break;
            default:
                UnityEngine.Debug.LogError($"DEFAULTED ON TILENAME BC COULDNT FIND {type}");
                name =  "_";
                break;
        }

        return name;
    }
    public static TileTypes tileType(string name) {
        TileTypes type = TileTypes.NORMAL;
        switch(name){
            case "_":
                type = TileTypes.NORMAL;
                break;
            case "F":
                type = TileTypes.FOREST;
                break;
            case "P":
                type = TileTypes.PLAYER;
                break;
            case "R":
                type = TileTypes.RIVER;
                break;
            case "M":
                type = TileTypes.MOUNTAIN;
                break;
            case "D":
                type = TileTypes.DESERT;
                break;
            case "T":
                type = TileTypes.TREASURE;
                break;
            case "QuestionMark":
                type = TileTypes.QUESTION_MARK;
                break;
            case "B":
                type = TileTypes.BRIDGE;
                break;
            case "X":
                type = TileTypes.NOTHING;
                break;
            default:
                UnityEngine.Debug.LogError($"DEFAULTED ON TILENAME BC COULDNT FIND {type}");
                type = TileTypes.NORMAL;
                break;
        }
        return type;
    }



}
