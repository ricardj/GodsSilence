using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{

    [Header("Partner mode ui")]
    public GameObject partnerModePanel;


    #region partner_mode
    bool partnerModeActive;
    public void ToogleParnterMode()
    {
        if(partnerModeActive)
        {
            partnerModePanel.SetActive(false);
            partnerModeActive = false;
        }else
        {
            partnerModeActive = true;
            partnerModePanel.SetActive(true);
        }
    }
    #endregion
}
