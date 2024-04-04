using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject baseUI;
    [SerializeField]
    private SaveAndLoad saveAndLoad;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) 
        {
            if (!GameManager.isPause)
                CallMenu();
            else
                CloseMenu();
        }
    }

    private void CallMenu()
    {
        GameManager.isPause = true;
        baseUI.SetActive(true);
        Time.timeScale = 0f;
    }

    private void CloseMenu()
    {
        GameManager.isPause = false;
        baseUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ClickSave()
    {
        saveAndLoad.SaveData();
    }

    public void ClickLoad()
    {
        saveAndLoad.LoadData();
    }

    public void ClickExit()
    {
        Application.Quit();
    }
}
