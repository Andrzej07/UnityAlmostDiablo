using System.Collections;
using System.Collections.Generic;
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
        DefenseController targetDefense = target.GetComponent<DefenseController>();
        CharacterStatistics stats = target.GetComponent<CharacterStatistics>();
        Healing healing = new Healing();
        healing.source = source;
        if(percentageHeal)
        {
            healing.amount = healAmount / 100 * stats.maxHealth;
        } else
        {
            healing.amount = healAmount + stats.strength * strengthCoefficient + stats.intelligence * intelligenceCoefficient;
        }
        targetDefense.ReceiveHealing(healing);
    }
}
