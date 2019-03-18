using System.Collections;
using System.Collections.Generic;
using Demo.Characters;
using Demo.Combat;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/Healing")]
public class HealingEffect : Effect
{
    public bool percentageHeal = false;
    public float healAmount = 10;
    public float strengthCoefficient;
    public float intelligenceCoefficient;

    public override void ApplyEffect(GameObject source, GameObject target)
    {
        IHealable healable = target.GetComponent<IHealable>();
        ICombatStatistics stats = target.GetComponent<ICombatStatistics>();
        Healing healing = new Healing();
        healing.source = source;
        if (percentageHeal)
        {
            healing.amount = healAmount / 100 * stats.Health.MaxHealth;
        }
        else
        {
            healing.amount = healAmount + stats.Strength * strengthCoefficient + stats.Intelligence * intelligenceCoefficient;
        }
        healable.ReceiveHealing(healing);
    }
}
