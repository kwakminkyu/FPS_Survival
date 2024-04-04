using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public string sceneName;

    public void ClickStart()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ClickLoad()
    {

    }

    public void ClickExit()
    {
        Application.Quit();
    }
}
