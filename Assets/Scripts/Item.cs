using System.Runtime.CompilerServices;
using System.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Item 
{

    public enum ItemKind {
        HOMING_PIDGEON,
        BINOCULARS,
        ANCESTRAL_KNOWLEDGE
    }

    private string image;

    private string name;

    private ItemKind kind;


    public ItemKind getKind()
    {
        return this.kind;
    }


    public static Item createItem(ItemKind kind){
        
        Item item = new Item();
        switch(kind){
            case ItemKind.ANCESTRAL_KNOWLEDGE:
                item.name = "Ancestral Knowledge";
                item.image = "";
                item.kind = kind;
                break;
            case ItemKind.BINOCULARS:
                item.name = "Binoculars";
                item.image = "";
                item.kind = kind;
            break;
            case ItemKind.HOMING_PIDGEON:
                item.name = "Homing Pidgeon";
                item.image = "";
                item.kind = kind;
            break;
        }
        return item;
    }
    public string getName()
    {
        return this.name;
    }


}