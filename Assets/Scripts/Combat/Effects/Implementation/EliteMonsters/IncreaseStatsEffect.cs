using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Elite Monster Effect/Increase stats")]
public class IncreaseStatsEffect : EliteMonsterEffect {
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
        CharacterStatistics cs = target.GetComponent<CharacterStatistics>();
        cs.maxMana += mana;
        cs.mana += mana;
        cs.maxHealth += health;
        cs.health += health;
        cs.armor += armor;
        cs.strength += strength;
        cs.intelligence += intelligence;
        cs.manaRegenerationRate += manaRegen;
        NavMeshAgent agent = target.GetComponent<NavMeshAgent>();
        agent.speed += speed;
    }
}
