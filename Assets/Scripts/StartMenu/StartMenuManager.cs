using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StartScreens
{
    PRESS_START,
    MAIN_MENU,
    CREDITS
}

public class StartMenuManager : MonoBehaviour
{
    [Header("Managers")]
    public StartMenuUIManager uiManager;

    [Header("Others")]
    public KeyCode pressStartKey = KeyCode.Space;
    public string newGameScene = "TwoGuysGameplay";

    [Range(0, 0.5f)]
    public float menuMouseMovementPadding = 0.4f;

    StartScreens currentScreen;
    Camera mainCamera;
    public void Start()
    {
        mainCamera = Camera.main;
        currentScreen = StartScreens.PRESS_START;      
    }

    

    public void Update()
    {
        switch(currentScreen)
        {
            case StartScreens.PRESS_START:
                PressStartInteraction();
                break;
            case StartScreens.MAIN_MENU:
                MainMenuInteraction();
                break;
            case StartScreens.CREDITS:
                CreditsInteraction();
                break;
                
        }

    }

    public void PressStartInteraction()
    {
        if(Input.GetKeyDown(pressStartKey))
        {
            uiManager.ShowMainMenu();
            currentScreen = StartScreens.MAIN_MENU;
        }
    }

    public void MainMenuInteraction()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            uiManager.MoveOptionUp();
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            uiManager.MoveOptionDown();

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            uiManager.ShowPressStart();
            currentScreen = StartScreens.PRESS_START;
        }
        Vector3 viewportPosition = mainCamera.ScreenToViewportPoint(Input.mousePosition);

        if (viewportPosition.y > 1 - menuMouseMovementPadding)
            uiManager.MoveOptionUp();
        if (viewportPosition.y < menuMouseMovementPadding)
            uiManager.MoveOptionDown();
    }


    public void OnNewGame()
    {
        TransitionManager.instance.ChangeToScene(newGameScene);
    }

    public void OnExit()
    {
        Application.Quit();
    }

    void CreditsInteraction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            uiManager.ShowMainMenu();
            currentScreen = StartScreens.MAIN_MENU;
        }
            

    }

    public void OnCredits()
    {
        currentScreen = StartScreens.CREDITS;
        //TODO: implement this action
        uiManager.ShowCredits();
    }
}
