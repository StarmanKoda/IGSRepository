using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System;

public class MainMenu : MonoBehaviour
{
    public void switchScene(string sceneName)
    {
        try
        {
            SceneManager.LoadScene(sceneName);
        }catch(Exception e)
        {
            Debug.LogException(e);
        }
    }

    public void toggleView(GameObject disable)
    {
        disable.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void quit()
    {
        Application.Quit();
    }
}
