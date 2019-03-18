
using System;
using UnityEngine;

namespace Demo.Combat
{
    public interface IDamageable
    {
        event Action<GameObject> DeathEvent;
        event Action<Damage> ReceiveDamageEvent;
        void ReceiveDamage(Damage damage);
    }
}