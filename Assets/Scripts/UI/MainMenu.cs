using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject[] views;
    public int curView = 0;
    public GameObject keyboard;
    public GameObject controller;
    bool onController = true;
    public Button keyBtn;
    public Button contrBtn;
    public GameObject[] firstSelect;

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

    public void switchControlType()
    {
        if (onController)
        {
            onController = false;
            controller.SetActive(false);
            keyboard.SetActive(true);
            keyBtn.interactable = false;
            contrBtn.interactable = true;
        }
        else
        {
            onController = true;
            controller.SetActive(true);
            keyboard.SetActive(false);
            keyBtn.interactable = true;
            contrBtn.interactable = false;
        }
    }

    public void toggleView(int newView)
    {
        views[newView].SetActive(true);
        views[curView].SetActive(false);
        curView = newView;
    }

    public void selectButton(GameObject button)
    {
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(button);
    }
    public void quit()
    {
        Application.Quit();
    }

    public void Update()
    {
        if (Input.GetButtonDown("Attack2"))
        {
            if(curView > 0)
            {
                toggleView(0);
                selectButton(firstSelect[0]);
            }
        }
        
        if(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject == null)
        {
            if (Input.GetAxis("Horizontal") > 0.1 || Input.GetAxis("Horizontal") < -0.1 || Input.GetAxis("Vertical") > 0.1 || Input.GetAxis("Vertical") < -0.1)
            {
                selectButton(firstSelect[curView]);
            }
        }
    }
}
