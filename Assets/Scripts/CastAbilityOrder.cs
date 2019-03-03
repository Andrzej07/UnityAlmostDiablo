using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastAbilityOrder : IOrder
{
    private Ability ability;
    private AbilityCaster abilityCaster;
    private GameObject target;
    private bool ignoreError;
    private bool abilityUsed = false;
    bool recovered = false;

    public CastAbilityOrder(AbilityCaster abilityCaster, Ability ability, GameObject target, bool ignoreError = false)
    {
        this.abilityCaster = abilityCaster;
        this.ability = ability;
        this.target = target;
        this.ignoreError = ignoreError;
    }

    public bool Finished
    {
        get
        {
            return recovered;
        }
    }

    public bool Interruptible
    {
        get
        {
            return !abilityUsed;
        }
    }
    public void Perform()
    {
        if (!abilityUsed)
        {
            bool result = abilityCaster.CastAbility(ability, target, ignoreError);
            if (result != abilityUsed)
            {
                abilityUsed = true;
                abilityCaster.StartCoroutine(StartAbilityRecovery(ability.recoveryTime));
            }
        }
    }

    IEnumerator StartAbilityRecovery(float time)
    {
        yield return new WaitForSeconds(time);
        recovered = true;
    }
}
