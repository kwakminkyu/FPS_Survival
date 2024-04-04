using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public string sceneName;

    public static Title Instance;

    private SaveAndLoad saveAndLoad;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
            Destroy(gameObject);
    }

    public void ClickStart()
    {
        SceneManager.LoadScene(sceneName);
        gameObject.SetActive(false);
    }

    public void ClickLoad()
    {
        StartCoroutine(LoadCoroutine());
    }

    private IEnumerator LoadCoroutine()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            yield return null;
        }

        saveAndLoad = FindAnyObjectByType<SaveAndLoad>();
        saveAndLoad.LoadData();
        gameObject.SetActive(false);
    }

    public void ClickExit()
    {
        Application.Quit();
    }
}
