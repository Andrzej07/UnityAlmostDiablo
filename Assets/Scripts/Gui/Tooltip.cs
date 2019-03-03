using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public string text;

    GuiController guiController;

    // Use this for initialization
    void Start () {
        guiController = GameObject.Find("GUI").GetComponent<GuiController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(text != null)
            guiController.ShowTooltip(text);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        guiController.HideTooltip();
    }
}
