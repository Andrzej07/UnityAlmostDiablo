using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseController : MonoBehaviour
{

    public delegate void OnReceiveDamageDelegate();
    public OnReceiveDamageDelegate receiveDamageDelegate;

    public delegate void OnReceiveHealingDelegate();
    public OnReceiveHealingDelegate receiveHealingDelegate;

    public delegate void OnDeathDelegate(GameObject gameObject);
    public OnDeathDelegate deathDelegate;

    [HideInInspector]
    public bool isDead;

    private CharacterStatistics characterStatistics;
    private Animator animator;
    private CombatTextSpawner combatTextSpawner;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        characterStatistics = GetComponent<CharacterStatistics>();
        isDead = false;
    }

    void Start()
    {
        combatTextSpawner = GameObject.Find("Combat Text").GetComponent<CombatTextSpawner>();
    }

    void Update()
    {

    }

    public void ReceiveDamage(Damage damage)
    {
        if (isDead)
            return;
        int finalDamageValue = Mathf.RoundToInt(Mathf.Max(1, damage.amount * (100 - characterStatistics.armor) / 100));
        Debug.Log(gameObject.name + " received " + finalDamageValue + " damage from " + damage.source.name);
        characterStatistics.health -= finalDamageValue;
        combatTextSpawner.SpawnText(transform, finalDamageValue);
        if (characterStatistics.statsChangeDelegate != null)
            characterStatistics.statsChangeDelegate();
        if (characterStatistics.health <= Mathf.Epsilon)
        {
            characterStatistics.health = 0;
            isDead = true;
            animator.SetBool("Dead", true);
            if (deathDelegate != null)
                deathDelegate(gameObject);
        }
        else
        {
            animator.SetTrigger("Damaged");
        }
    }

    public void ReceiveHealing(Healing healing)
    {
        if (isDead)
            return;
        int finalHealingValue = Mathf.RoundToInt(Mathf.Min(healing.amount, characterStatistics.maxHealth - characterStatistics.health));
        characterStatistics.health += finalHealingValue;
        if (characterStatistics.statsChangeDelegate != null)
            characterStatistics.statsChangeDelegate();
        combatTextSpawner.SpawnText(transform, Mathf.RoundToInt(finalHealingValue), Color.green);
    }
}
