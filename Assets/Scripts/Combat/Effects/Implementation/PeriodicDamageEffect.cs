using System.Collections;
using System.Collections.Generic;
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
        DefenseController defense = target.GetComponent<DefenseController>();
        Damage damage = new Damage()
        {
            amount = damageAmount,
            source = source
        };
        defense.ReceiveDamage(damage);
    }
}
