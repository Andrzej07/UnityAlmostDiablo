using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Demo.Abilities;
using Demo.Characters;

[CreateAssetMenu(menuName = "Elite Monster Effect/Grant ability")]
public class GrantAbilityEffect : EliteMonsterEffect
{
    public Ability ability;
    public override void ApplyEffect(GameObject source, GameObject target)
    {
        base.ApplyEffect(source, target);
        AbilitiesController ac = target.GetComponent<AbilitiesController>();
        Ability copy = Instantiate(ability);
        ac.AddAbility(copy);
        ManaUsage manaUsage = copy.GetComponent<ManaUsage>();
        if (manaUsage != null)
        {
            ICombatStatistics stats = target.GetComponent<ICombatStatistics>();
            if (stats.Mana.MaxMana < manaUsage.manaCost)
            {
                stats.Mana.MaxMana = manaUsage.manaCost;
            }
        }
    }
}
