using System.Collections;
using System.Collections.Generic;
using Demo.Characters;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Elite Monster Effect/Increase stats")]
public class IncreaseStatsEffect : EliteMonsterEffect
{
    public int strength;
    public int intelligence;
    public int armor;
    public int health;
    public int mana;
    public float manaRegen;
    public float speed;
    public override void ApplyEffect(GameObject source, GameObject target)
    {
        base.ApplyEffect(source, target);
        ICombatStatistics cs = target.GetComponent<ICombatStatistics>();
        cs.Mana.IncreaseMaxAndCurrent(mana);
        cs.Health.IncreaseMaxAndCurrent(health);
        cs.Armor += armor;
        cs.Strength += strength;
        cs.Intelligence += intelligence;
        cs.Mana.RegenerationRate += manaRegen;
        NavMeshAgent agent = target.GetComponent<NavMeshAgent>();
        agent.speed += speed;
    }
}
