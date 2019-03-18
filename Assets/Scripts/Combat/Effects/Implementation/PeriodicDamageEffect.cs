using System.Collections;
using System.Collections.Generic;
using Demo.Combat;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/Periodic Damage")]
public class PeriodicDamageEffect : PeriodicEffect
{
    public float damageAmount;

    public override void OnRemove(GameObject source, GameObject target)
    {
        Tick(source, target);
    }

    public override void Tick(GameObject source, GameObject target)
    {
        IDamageable defense = target.GetComponent<IDamageable>();
        Damage damage = new Damage()
        {
            amount = damageAmount,
            source = source
        };
        defense.ReceiveDamage(damage);
    }
}
