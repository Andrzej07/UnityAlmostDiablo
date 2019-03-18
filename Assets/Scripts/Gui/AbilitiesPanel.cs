using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Demo.Abilities;
using System.Collections.ObjectModel;

public class AbilitiesPanel : MonoBehaviour
{

    public AbilitiesController playerAbilities;
    public AbilitySlot[] abilitySlots;

    private void Awake()
    {
        playerAbilities.AbilitiesChangeEvent += OnAbilitiesChangedEvent;
    }

    void OnAbilitiesChangedEvent()
    {
        ReadOnlyCollection<Ability> abilities = playerAbilities.abilities;
        for (int i = 0; i < abilities.Count && i < abilitySlots.Length; i++)
        {
            abilitySlots[i].SlotAbility(abilities[i]);
        }
    }

}
