using System.Collections;
using System.Collections.Generic;
using Demo.Abilities;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    string text;

    GuiController guiController;

    void Awake()
    {
        AbilitySlot slot = GetComponent<AbilitySlot>();
        slot.AbilitySlottedEvent += OnAbilitySlottedEvent;
    }

    void Start()
    {
        guiController = GameController.instance.guiController;
    }

    void OnAbilitySlottedEvent(Ability ability)
    {
        text = ability.Tooltip;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (text != null)
            guiController.ShowTooltip(text);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        guiController.HideTooltip();
    }
}
