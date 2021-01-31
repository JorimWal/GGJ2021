using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    new RectTransform rect, childRect;
    private void Start()
    {
        rect = GetComponent<RectTransform>();
        childRect = transform.GetChild(0).GetComponent<RectTransform>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 sizeDelta = childRect.sizeDelta;
        //set anchor according to mouse position
        if ((Screen.width - Input.mousePosition.x) < sizeDelta.x)
            rect.pivot = new Vector2(1, rect.pivot.y);
        else
            rect.pivot = new Vector2(0, rect.pivot.y);

        if ((Screen.height - Input.mousePosition.y) < sizeDelta.y)
            rect.pivot = new Vector2(rect.pivot.x, 1);
        else
            rect.pivot = new Vector2(rect.pivot.x, 0);

        Debug.Log("pivot :" + rect.pivot);
        Debug.Log("sizeDelta: " + rect.sizeDelta);
        transform.position = Input.mousePosition;
    }
}
