using System;
using System.Collections;
using System.Collections.Generic;
using Demo.Characters;
using Demo.Combat;
using UnityEngine;

public class DefenseController : MonoBehaviour, IDamageable, IHealable
{
    [HideInInspector]
    public bool isDead;

    private ICombatStatistics characterStatistics;
    private CombatTextSpawner combatTextSpawner;

    public event Action<GameObject> DeathEvent;
    public event Action<Damage> ReceiveDamageEvent;
    public event Action<Healing> ReceiveHealingEvent;

    private void Awake()
    {
        characterStatistics = GetComponent<ICombatStatistics>();
        isDead = false;
    }

    void Start()
    {
        characterStatistics.Health.HealthDepletedEvent += OnHealthDepletedEvent;
        combatTextSpawner = GameController.instance.guiController.CombatTextSpawner;
    }

    public void ReceiveDamage(Damage damage)
    {
        if (isDead)
            return;
        if (ReceiveDamageEvent != null)
            ReceiveDamageEvent(damage);
        int finalDamageValue = Mathf.RoundToInt(Mathf.Max(1, damage.amount * (100 - characterStatistics.Armor) / 100));
        Debug.Log(gameObject.name + " received " + finalDamageValue + " damage from " + damage.source.name);
        characterStatistics.Health.DealDamage(finalDamageValue);
        combatTextSpawner.SpawnText(transform, finalDamageValue);
    }

    public void ReceiveHealing(Healing healing)
    {
        if (isDead)
            return;
        if (ReceiveHealingEvent != null)
            ReceiveHealingEvent(healing);
        int finalHealingValue = characterStatistics.Health.RestoreHealth((int)healing.amount);
        combatTextSpawner.SpawnText(transform, finalHealingValue, Color.green);
    }

    void OnHealthDepletedEvent()
    {
        isDead = true;
        if (DeathEvent != null)
            DeathEvent(gameObject);
    }
}
