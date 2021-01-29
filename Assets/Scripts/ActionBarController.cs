﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionBarController : MonoBehaviour
{
    [HideInInspector]
    public string ActionInput {
        set {
            //Reset the Action Bar
            foreach (Transform child in transform)
                Destroy(child.gameObject);
            foreach(char action in value)
            {
                //create a button image
                GameObject instance = new GameObject(action.ToString());
                RectTransform rect = instance.AddComponent<RectTransform>();
                rect.SetParent(transform);
                rect.sizeDelta = new Vector2(100, 100);
                Image image = instance.AddComponent<Image>();
                Sprite sprite;
                switch(action)
                {
                    case 'U':
                        image.sprite = up;
                        break;
                    case 'D':
                        image.sprite = down;
                        break;
                    case 'L':
                        image.sprite = left;
                        break;
                    case 'R':
                        image.sprite = right;
                        break;
                    default:
                        image.sprite = empty;
                        break;
                }
            }
        }
    }

    Sprite up, down, left, right, empty;

    public void Start()
    {
        //Load the button sprites from resources
        up = Resources.Load<Sprite>("ActionButtons/UpArrowButton");
        down = Resources.Load<Sprite>("ActionButtons/DownArrowButton");
        left = Resources.Load<Sprite>("ActionButtons/LeftArrowButton");
        right = Resources.Load<Sprite>("ActionButtons/RightArrowButton");
        empty = Resources.Load<Sprite>("ActionButtons/EmptyButton");

        ActionInput = "ULLURD";
    }
}
