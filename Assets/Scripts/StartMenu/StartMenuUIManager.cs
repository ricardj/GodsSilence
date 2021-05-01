using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class StartMenuUIManager : MonoBehaviour
{

    public GameObject pressStartPanel;
    public GameObject mainMenuPanel;
    public GameObject creditsPanel;
    public CircleController circleController;

    [Header("Main menu")]
    public RectTransform[] optionPivots;
    public float optionAngleDifference = 30;
    List<GameObject> uiPanels;

    

    private void Start()
    {
        uiPanels = new List<GameObject>();
        uiPanels.Add(pressStartPanel);
        uiPanels.Add(mainMenuPanel);
        uiPanels.Add(creditsPanel);
    }

    public void ShowMainMenu()
    {
        DeactivateAllPanels();
        mainMenuPanel.SetActive(true);
        circleController.SetMainMenuCircle();
    }

    public void ShowPressStart()
    {
        DeactivateAllPanels();
        pressStartPanel.SetActive(true);
        circleController.SetStartScreenCircle();
    }

    public void ShowCredits()
    {
        DeactivateAllPanels();
        creditsPanel.SetActive(true);
        circleController.SetCreditsCircle();
    }

    public void DeactivateAllPanels()
    {
        uiPanels.ForEach(uiPanel => uiPanel.SetActive(false));
    }


    #region press_start

    #endregion

    #region main_menu
    int currentOption = 0;
    bool animationBlock;
    public void SetMainMenuOption(int menuOption)
    {
        
        if (menuOption >= optionPivots.Length || menuOption < 0)
            return;
        if (animationBlock)
            return;
        animationBlock = true;
        float totalAngles = -(menuOption - currentOption) * optionAngleDifference;
        for (int i = 0; i < optionPivots.Length; i++)
        {
            optionPivots[i].DORotate(new Vector3(0, 0, totalAngles), 0.3f).SetRelative().OnComplete(()=>animationBlock = false);
        }
        currentOption = menuOption;
    }

    public void MoveOptionUp()
    {
        SetMainMenuOption(currentOption -1);
        
    }

    public void MoveOptionDown()
    {
        SetMainMenuOption(currentOption + 1);
    }


    #endregion
}
