using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public static DialogueController Instance;
    public TextBubble textBubble;
    Map.MapType mapType;

    Color gold, red, green;
    void Start()
    {
        //Singleton Instance
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
        mapType = Map.Instance.map;
        Color gold = new Color(1, 195f / 255f, 18f / 255f);
        Color green = new Color(0, 148f / 255f, 50f / 255f);
        Color red = new Color(234f / 255f, 32f / 255f, 39f / 255f);
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
            case Map.MapType.Tutorial2:
                textBubble.SetContent("The signpost was a complete waste of time, however");
                textBubble.SetContent(" upon leaving the village completely voluntarily and");
                textBubble.SetContent(" NOT being kicked out for drunken behaviour, I noticed");
                textBubble.SetContent(" a scrap of parchment describing a small area of land.");
                textBubble.SetContent("Could this be an actual treasure map?");
                textBubble.SetContent("The adventure continues!");
                textBubble.OnFinishQueue.AddListener(SwitchSceneToRandom);
                break;
            default:
                textBubble.SetContent("After countless hours of digging, your workers spot a glint of gold. You have found the golden city El Dorado!", gold.r, gold.g, gold.b);

                break;
        }
    }

    public void LossMessage(bool diedFromWorkers)
    {
        if(diedFromWorkers)
        {
            textBubble.SetContent("All your workers have perished, your search is over", red.r, red.g, red.b);
        }
        else
        {
            textBubble.SetContent("Time moves ever forward, and time has run out.  Your search is over.", red.r, red.g, red.b);
        }
        switch (mapType)
        {
            case Map.MapType.Tutorial1:
                textBubble.OnFinishQueue.AddListener(SwitchSceneToTutorial1);
                break;
            case Map.MapType.Tutorial2:
                textBubble.OnFinishQueue.AddListener(SwitchSceneToTutorial2);
                break;
            case Map.MapType.Random:
                textBubble.OnFinishQueue.AddListener(SwitchSceneToRandom);
                break;
            default:
                textBubble.OnFinishQueue.AddListener(SwitchSceneToMainMenu);
                break;
        }
    }

    public void SuccesfulExpeditionMessage()
    {
        textBubble.SetContent("Your worker brings new insight of your surroundings");
    }

    public void WorkerDeathMessage(bool pigeon)
    {
        if(!pigeon)
            textBubble.SetContent("Your worker did not return from their expedition", red.r, red.g, red.b);
        else
        {
            switch (mapType)
            {
                case Map.MapType.Tutorial2:
                    textBubble.SetContent("A homing pigeon flew back with information of a river", red.r, red.g, red.b);
                    textBubble.SetContent(", which they then promptly drowned in. Perhaps we should ", red.r, red.g, red.b);
                    textBubble.SetContent(" look for a forest so we can get lumber to build a bridge!", red.r, red.g, red.b);
                    break;
                default:
                    textBubble.SetContent("Your worker died in a river, but sent a homing pigeon informing you of the danger.", red.r, red.g, red.b);
                    break;
            }
        }
    }

    public void TreasureMessage(Item item)
    {
        switch (item.getKind())
        {
            case Item.ItemKind.HOMING_PIDGEON:
                PigeonMessage();
                break;
            default:
                textBubble.SetContent("Your workers find a chest containing useful supplies!", green.r, green.g, green.b);
                break;
        }
    }

    public void PigeonMessage()
    {
        switch (mapType)
        {
            case Map.MapType.Tutorial2:
                textBubble.SetContent("During their expedition, your workers have found a chest", green.r, green.g, green.b);
                textBubble.SetContent(" containing a homing pigeon. Now atleast I will know what", green.r, green.g, green.b);
                textBubble.SetContent(" happened to them if they inevitably die in our crucial expedition.", green.r, green.g, green.b);
                break;
            default:
                textBubble.SetContent("During their expedition, your workers have found a chest", green.r, green.g, green.b);
                textBubble.SetContent(" containing a homing pigeon. Now atleast I will know what", green.r, green.g, green.b);
                textBubble.SetContent(" happened to them if they die in a river or ditch.", green.r, green.g, green.b);
                break;
        }
    }

    public void RevealedChestMessage()
    {
        textBubble.SetContent("Your workers explains they found a chest, perhaps you should send them back to open it.");
    }

    public void WoodMessage()
    {
        switch (mapType)
        {
            case Map.MapType.Tutorial2:
                textBubble.SetContent("The men have returned with plentiful lumber.");
                textBubble.SetContent("We can use this to build a bridge over the river!");
                break;
            default:
                textBubble.SetContent("Your workers found a forest and have stripped it of it's lumber.");
                break;
        } 
    }
    public void WrongDigMessage()
    {
        textBubble.SetContent("El Dorado was not laying in this location.");
    }

    public void SwitchSceneToTutorial1()
    {
        SceneManagement.SwitchScene(1);
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
