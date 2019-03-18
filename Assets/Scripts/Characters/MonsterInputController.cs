using System.Collections;
using System.Collections.Generic;
using Demo.Abilities;
using Demo.Combat;
using UnityEngine;

namespace Demo.Characters
{
    public class MonsterInputController : MonoBehaviour
    {
        AbilitiesController abilitiesController;

        void Awake()
        {
            GetComponent<IDamageable>().DeathEvent += OnDeathEvent;
            abilitiesController = GetComponent<AbilitiesController>();
            abilitiesController.AbilityActivatedEvent += () =>
            {
                enabled = false;
            };
            abilitiesController.AbilityDeactivatedEvent += OnAbilityDeactivatedEvent;
        }

        void Start()
        {
            MonsterController mc = GetComponent<MonsterController>();
            mc.player.GetComponent<IDamageable>().DeathEvent += OnDeathEvent;
        }

        void Update()
        {
            for (int i = abilitiesController.abilities.Count - 1; i >= 0; i--)
            {
                Ability ability = abilitiesController.abilities[i];
                abilitiesController.ActivateAbility(ability);
            }
        }

        void OnDeathEvent(GameObject ded)
        {
            enabled = false;
            abilitiesController.AbilityDeactivatedEvent -= OnAbilityDeactivatedEvent;
        }

        void OnAbilityDeactivatedEvent()
        {
            enabled = true;
        }
    }
}