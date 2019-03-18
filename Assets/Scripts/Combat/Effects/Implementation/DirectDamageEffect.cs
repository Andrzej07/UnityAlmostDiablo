using System.Collections;
using System.Collections.Generic;
using Demo.Characters;
using Demo.Combat;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/Direct Damage")]
public class DirectDamageEffect : Effect
{
    public float damageAmount;
    public float strengthCoefficient;
    public float intelligenceCoefficient;

    public override void ApplyEffect(GameObject source, GameObject target)
    {
        IDamageable damageable = target.GetComponent<IDamageable>();
        ICombatStatistics stats = source.GetComponent<ICombatStatistics>();
        Damage damage = new Damage()
        {
            source = source,
            amount = damageAmount + stats.Strength * strengthCoefficient + stats.Intelligence * intelligenceCoefficient
        };
        damageable.ReceiveDamage(damage);
    }
}
