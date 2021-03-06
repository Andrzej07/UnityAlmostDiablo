using System.Collections;
using System.Collections.Generic;
using Demo.Characters;
using UnityEngine;

public class EliteMonsterEffect : Effect
{
    public int experienceBonus;

    public override void ApplyEffect(GameObject source, GameObject target)
    {
        IncreseExperienceGain(target);
    }

    void IncreseExperienceGain(GameObject target)
    {
        target.GetComponent<IExperienceReward>().ExperienceAmount += experienceBonus;
    }
}
