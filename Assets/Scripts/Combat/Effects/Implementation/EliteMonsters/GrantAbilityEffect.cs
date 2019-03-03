using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Elite Monster Effect/Grant ability")]
public class GrantAbilityEffect : EliteMonsterEffect
{
    public Ability ability;
    public override void ApplyEffect(GameObject source, GameObject target)
    {
        base.ApplyEffect(source, target);
        AbilityCaster ac = target.GetComponent<AbilityCaster>();
        ac.AddAbility(ability);
        CharacterStatistics cs = target.GetComponent<CharacterStatistics>();
        if (cs.maxMana < ability.manaCost)
        {
            cs.maxMana += ability.manaCost;
            cs.mana += ability.manaCost;
        }
        if(cs.manaRegenerationRate < 0.5)
            cs.manaRegenerationRate += 0.5f;
    }
}
