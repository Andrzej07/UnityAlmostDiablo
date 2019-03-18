using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiController : MonoBehaviour
{
    [SerializeField]
    GameObject gameOverPopup;
    [SerializeField]
    GameObject statUpPanel;
    [SerializeField]
    GameObject tooltipPanel;
    [SerializeField]
    CombatTextSpawner combatTextSpawner;
    public CombatTextSpawner CombatTextSpawner
    {
        get
        {
            return combatTextSpawner;
        }
    }

    public void ShowGameOverPopup()
    {
        gameOverPopup.SetActive(true);
    }

    public void ShowStatUpPanel()
    {
        statUpPanel.SetActive(true);
    }

    public void HideStatUpPanel()
    {
        statUpPanel.SetActive(false);
    }

    public void ShowTooltip(string text)
    {
        tooltipPanel.SetActive(true);
        tooltipPanel.GetComponentInChildren<Text>().text = text;
    }

    public void HideTooltip()
    {
        tooltipPanel.SetActive(false);
    }

}
