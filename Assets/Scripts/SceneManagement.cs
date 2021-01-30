using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public static void SwitchScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public static void SwitchScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
