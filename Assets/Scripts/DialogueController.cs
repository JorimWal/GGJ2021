using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public static DialogueController Instance;
    public TextBubble textBubble;
    Map.MapType mapType;

    Color gold, red;
    void Start()
    {
        //Singleton Instance
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
        mapType = Map.Instance.map;
        Color gold = new Color(1, 195f / 255f, 18f / 255f);
    }
    public void VictoryMessage()
    {
        switch (mapType)
        {
            case Map.MapType.Tutorial1:
                textBubble.SetContent("There was no real treasure map buried here,");
                textBubble.SetContent(" but we did find a wooden sign saying something about");
                textBubble.SetContent(" a Dorado. This must be what the locals didn't talk about!");
                textBubble.SetContent("We continue to head north! To adventure!");
                textBubble.OnFinishQueue.AddListener(SwitchSceneToTutorial2);
                break;
            default:
                textBubble.SetContent("After countless hours of digging, your workers spot a glint of gold. You have found the golden city El Dorado!", gold.r, gold.g, gold.b);

                break;
        }
    }

    public void SwitchSceneToTutorial2()
    {
        SceneManagement.SwitchScene(2);
    }

    public void SwitchSceneToRandom()
    {
        SceneManagement.SwitchScene(3);
    }

    public void SwitchSceneToMainMenu()
    {
        SceneManagement.SwitchScene(0);
    }
}
