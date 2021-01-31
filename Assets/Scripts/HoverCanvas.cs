using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class HoverCanvas : MonoBehaviour
{
    public Grid grid;
    public Tilemap tilemap;
    new RectTransform rect;
    Text title, body;
    private void Start()
    {
        rect = GetComponent<RectTransform>();
        title = rect.GetChild(0).GetComponent<Text>();
        body = rect.GetChild(1).GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 sizeDelta = Vector2.zero;
        for(int i = 0; i < rect.childCount; i++)
        {
            sizeDelta += rect.GetChild(i).GetComponent<RectTransform>().sizeDelta;
        }
        //set anchor according to mouse position
        if ((Screen.width - Input.mousePosition.x) < sizeDelta.x)
            rect.pivot = new Vector2(1, rect.pivot.y);
        else
            rect.pivot = new Vector2(0, rect.pivot.y);

        if ((Screen.height - Input.mousePosition.y) < sizeDelta.y)
            rect.pivot = new Vector2(rect.pivot.x, 1);
        else
            rect.pivot = new Vector2(rect.pivot.x, 0);

        transform.position = Input.mousePosition;

        if(grid != null)
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x , Input.mousePosition.y, 13.8f));
            Vector3Int gridCoordinate = grid.WorldToCell(mouseWorldPos);
            Vector3Int tileCoordinate = tilemap.WorldToCell(mouseWorldPos);
            title.text = mouseWorldPos.ToString();
            body.text = "Tile: " + tileCoordinate.ToString() + ", Grid: " + gridCoordinate.ToString();
        }
    }
}
