using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TextBubble : MonoBehaviour
{
    Text text;
    string content = "";
    System.Collections.Generic.Queue<(string, Color)> queue = new Queue<(string, Color)>();

    UnityEvent onFinishQueue;
    public UnityEvent OnFinishQueue {
        get {
            if (onFinishQueue == null)
                onFinishQueue = new UnityEvent();
            return onFinishQueue;
        }
    }

    private void Start()
    {
        
    }

    public void SetContent(string content, float colorR = 1, float colorG = 1, float colorB = 1, bool queueMessage = true)
    {
        if (text == null)
            text = GetComponentInChildren<Text>();
        
        if ((!queueMessage) || this.content == "")
        {
            this.content = content;
            text.color = new Color(colorR, colorG, colorB, 1);
            StopAllCoroutines();
            EmptyBubble();
            StartCoroutine(ScrollText());
        }
        else
        {
            queue.Enqueue((content, new Color(colorR, colorG, colorB)));
        }
    }

    public void EmptyBubble()
    {
        text.text = "";
    }

    const float timePerLetter = 0.04f;
    const float timeBetweenMessages = 1.5f;

    IEnumerator ScrollText()
    {
        while (true)
        {
            int index = 0;

            //This shouldn't be necessary, but make sure you can't have invisible text
            if (text.color == Color.black)
                text.color = Color.white;

            while (index < content.Length)
            {
                text.text = text.text + content[index];
                index++;
                yield return new WaitForSeconds(timePerLetter);
            }
            if (queue.Count <= 0)
            {
                content = "";
                yield return new WaitForSeconds(timeBetweenMessages);
                OnFinishQueue.Invoke();
                break;
            }
            yield return new WaitForSeconds(timeBetweenMessages);
            (string, Color) nextMessage = queue.Dequeue();
            content = nextMessage.Item1;
            text.color = nextMessage.Item2;
            text.text = "";
        }
    }
}
