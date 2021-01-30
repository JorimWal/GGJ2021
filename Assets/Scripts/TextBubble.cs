using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBubble : MonoBehaviour
{
    Text text;
    string content = "";

    public void SetContent(string content, float colorR = 1, float colorG = 1, float colorB = 1)
    {
        if (text == null)
            text = GetComponentInChildren<Text>();
        this.content = content;
        text.color = new Color(colorR, colorG, colorB, 1);
        EmptyBubble();
        StartCoroutine(ScrollText());
    }

    public void EmptyBubble()
    {
        text.text = "";
    }

    const float timePerLetter = 0.08f;

    IEnumerator ScrollText()
    {
        int index = 0;
        while (index < content.Length)
        {
            text.text = text.text + content[index];
            index++;
            yield return new WaitForSeconds(timePerLetter);
        }
    }
}
