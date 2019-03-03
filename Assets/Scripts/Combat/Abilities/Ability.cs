using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Ability : ScriptableObject
{

    public string abilityName;
    [TextArea(3, 10)]
    public string tooltip;
    public Effect[] effects;
    public float cooldown;
    public float manaCost;
    public float recoveryTime;

    public bool Use(GameObject source, GameObject target)
    {
        if (IsInPosition(source, target))
        {
            if (TriggerEffect(source, target))
            {
                AbilityCaster abilityCaster = source.GetComponent<AbilityCaster>();
                TriggerManaCost(abilityCaster);
                TriggerCooldown(abilityCaster);
                Animator animator = source.GetComponentInChildren<Animator>();
                animator.SetTrigger("Attack");
                return true;
            }
        }
        else
        {
            GetInPosition(source, target);
        }
        return false;
    }

    protected abstract bool TriggerEffect(GameObject source, GameObject target);

    protected void TriggerCooldown(AbilityCaster abilityCaster)
    {
        abilityCaster.TriggerCooldown(this);
    }

    protected void TriggerManaCost(AbilityCaster abilityCaster)
    {
        abilityCaster.TriggerManaCost(this);
    }

    protected abstract void GetInPosition(GameObject source, GameObject target);

    public abstract bool IsInPosition(GameObject source, GameObject target);

    // TODO: Generate tooltip based on attached effects
    public string GetTooltip()
    {
        return null;
    }
}
