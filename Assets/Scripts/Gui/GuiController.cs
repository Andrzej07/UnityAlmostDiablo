using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiController : MonoBehaviour {
    public GameObject gameOverPopup;
    public GameObject statUpPanel;
    public GameObject tooltipPanel;
    public Text errorText;

    private const float errorTimeout = 2;
    private float errorTimeRemaining = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (errorTimeRemaining > 0)
            errorTimeRemaining -= Time.deltaTime;
        else
            errorText.text = null;
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

    public void DisplayError(string text)
    {
        errorText.text = text;
        errorTimeRemaining = errorTimeout;
    }
}
