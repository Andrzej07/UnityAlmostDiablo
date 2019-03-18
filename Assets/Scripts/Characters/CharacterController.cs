using System;
using System.Collections;
using System.Collections.Generic;
using Demo.Abilities;
using Demo.Characters;
using Demo.Combat;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(DefenseController))]
[RequireComponent(typeof(AbilitiesController))]
[RequireComponent(typeof(NavMeshAgent))]
public class CharacterController : MonoBehaviour, ICharacterMovement, ICharacterCombat, ICombatStatistics
{
    public event Action StatsChangeEvent;

    protected NavMeshAgent agent;
    protected AbilitiesController abilitiesController;
    protected Animator animator;

    [Header("Combat statistics")]
    [SerializeField]
    private Mana mana;
    public Mana Mana
    {
        get
        {
            return mana;
        }
    }

    [SerializeField]
    private Health health;
    public Health Health
    {
        get
        {
            return health;
        }
    }

    [SerializeField]
    private int armor;
    public int Armor
    {
        get
        {
            return armor;
        }

        set
        {
            armor = value;
        }
    }

    [SerializeField]
    private int strength;
    public int Strength
    {
        get
        {
            return strength;
        }

        set
        {
            strength = value;
        }
    }

    [SerializeField]
    private int intelligence;
    public int Intelligence
    {
        get
        {
            return this.intelligence;
        }

        set
        {
            this.intelligence = value;
        }
    }

    protected void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        abilitiesController = GetComponent<AbilitiesController>();
        animator = GetComponentInChildren<Animator>();
        IDamageable damageable = GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.ReceiveDamageEvent += (Damage damage) =>
            {
                animator.SetTrigger("Damaged");
            };
            damageable.DeathEvent += (GameObject gameObject) =>
            {
                animator.SetBool("Dead", true);
            };
        }
    }

    protected void Update()
    {
        animator.SetBool("Run", agent.velocity.magnitude > 0);
        Mana.Update(Time.deltaTime);
    }

    protected void LateUpdate()
    {
        if (StatsChangeEvent != null)
            StatsChangeEvent();
    }

    public void MoveTo(Vector3 point)
    {
        agent.destination = point;
        agent.stoppingDistance = 0;
    }

    public void Stop()
    {
        agent.destination = transform.position;
    }

    public void ActivateAbility(int index)
    {
        if (index < abilitiesController.abilities.Count)
        {
            Ability ability = abilitiesController.abilities[index];
            abilitiesController.ActivateAbility(ability);
        }
    }

    public void Attack(GameObject target)
    {
        abilitiesController.ActivateAbility(abilitiesController.abilities[0]);
    }

    public void Teleport(Vector3 point)
    {
        agent.Warp(point);
        agent.destination = point;
    }
}
