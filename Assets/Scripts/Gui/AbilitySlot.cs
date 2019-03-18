using UnityEngine;
using UnityEngine.UI;
using Demo.Abilities;
using System.Collections;
using System;

public class AbilitySlot : MonoBehaviour
{
    public string key;
    public Text abilityLabel;
    public Text cooldownLabel;

    public Action<Ability> AbilitySlottedEvent;

    IAbilityCooldown subscribedCooldown;

    public void SlotAbility(Ability ability)
    {
        Debug.Log("Slotting " + ability.abilityName);
        if (subscribedCooldown != null)
            subscribedCooldown.RemaingingCooldownChangedEvent -= OnCooldownChangeEvent;

        if (AbilitySlottedEvent != null)
            AbilitySlottedEvent(ability);

        abilityLabel.text = ability.abilityName;
        IAbilityCooldown cooldown = ability.gameObject.GetComponent<IAbilityCooldown>();
        if (cooldown != null)
        {
            cooldown.RemaingingCooldownChangedEvent += OnCooldownChangeEvent;
            subscribedCooldown = cooldown;
        }
    }

    void OnCooldownChangeEvent(float value)
    {
        if (value > float.Epsilon)
            cooldownLabel.text = value.ToString("0.#");
        else
            cooldownLabel.text = null;
    }
}