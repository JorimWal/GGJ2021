using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionBarController : MonoBehaviour
{
    //Singleton Instance
    public static ActionBarController Instance;

    [HideInInspector]
    public string ActionInput {
        set {
            //Reset the Action Bar
            foreach (Transform child in transform)
                Destroy(child.gameObject);
            string onlyShowFiveChars = value.Substring(value.Length -5 < 0 ? 0 : (value.Length-5));
            Debug.Log($"ONLY SHOWING {onlyShowFiveChars} OF {value}");
            foreach(char action in onlyShowFiveChars)
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
                    case 'F':
                        image.sprite = dig;
                        break;
                    default:
                        image.sprite = empty;
                        break;
                }
            }
        }
    }

    Sprite up, down, left, right, empty, dig;

    public void Start()
    {
        //Singleton Instance
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        empty = Resources.Load<Sprite>("ActionButtons/Transparent");
        //Load the button sprites from resources
        Sprite[] spriteSheet = Resources.LoadAll<Sprite>("ActionButtons/Tiles");
        up = spriteSheet[13];
        down = spriteSheet[15];
        left = spriteSheet[12];
        right = spriteSheet[14];
        dig = spriteSheet[2];

        ActionInput = "";
    }
}
