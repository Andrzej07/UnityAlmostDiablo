using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityContainer {

    public Ability ability;

    private float cooldownTimer;
    private GameObject parent;
    private CharacterStatistics stats;

	public AbilityContainer(GameObject parent, Ability ability)
    {
        this.parent = parent;
        this.ability = ability;
        stats = parent.GetComponent<CharacterStatistics>();
        cooldownTimer = 0;
    }

    public void Update(float deltaTime)
    {
        cooldownTimer -= deltaTime;
    }

    public bool IsReady()
    {
        return cooldownTimer < Mathf.Epsilon;
    }
    
    public bool HasMana()
    {
        return ability.manaCost <= stats.mana;
    }

    public bool UseAbility(GameObject target)
    {                
        return ability.Use(parent, target);
    }

    public void TriggerCooldown()
    {
        cooldownTimer = ability.cooldown;
    }

    public void TriggerManaCost()
    {
        stats.mana -= ability.manaCost;
    }

    public float GetRemainingCooldown()
    {
        return Mathf.Max(0, cooldownTimer);
    }
}
