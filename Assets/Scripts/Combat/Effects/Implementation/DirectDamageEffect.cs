using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/Direct Damage")]
public class DirectDamageEffect : Effect
{
    public float damageAmount;
    public float strengthCoefficient;
    public float intelligenceCoefficient;

    public override void ApplyEffect(GameObject source, GameObject target)
    {
        DefenseController targetDefense = target.GetComponent<DefenseController>();
        CharacterStatistics stats = source.GetComponent<CharacterStatistics>();
        Damage damage = new Damage()
        {
            source = source,
            amount = damageAmount + stats.strength * strengthCoefficient + stats.intelligence * intelligenceCoefficient
        };
        targetDefense.ReceiveDamage(damage);
    }
}
