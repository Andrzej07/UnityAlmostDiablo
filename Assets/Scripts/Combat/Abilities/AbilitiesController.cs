using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.AI;

namespace Demo.Abilities
{
    public class AbilitiesController : MonoBehaviour
    {
        [HideInInspector]
        List<Ability> _abilities;
        public ReadOnlyCollection<Ability> abilities
        {
            get { return _abilities.AsReadOnly(); }
        }

        public event Action AbilitiesChangeEvent;

        public event Action AbilityActivatedEvent;
        public event Action AbilityDeactivatedEvent;

        void Awake()
        {
            UpdateAbilityList();
        }

        void Start()
        {
            if (AbilitiesChangeEvent != null)
                AbilitiesChangeEvent();
        }

        public void ActivateAbility(Ability ability)
        {
            if (!_abilities.Contains(ability))
            {
                Debug.LogWarning("Casting ability not slotted in controller!");
            }
            if (!ability.CheckRequirements())
                return;
            ability.gameObject.SetActive(true);
            if (AbilityActivatedEvent != null)
                AbilityActivatedEvent();
            Action deactivationHandler = null;
            deactivationHandler = () =>
            {
                if (AbilityDeactivatedEvent != null)
                    AbilityDeactivatedEvent();
                ability.DeactivatedEvent -= deactivationHandler;
            };
            ability.DeactivatedEvent += deactivationHandler;
        }

        void UpdateAbilityList()
        {
            _abilities = new List<Ability>(GetComponentsInChildren<Ability>(true));
            foreach (Ability ability in _abilities)
            {
                ability.gameObject.SetActive(false);
            }
            if (AbilitiesChangeEvent != null)
                AbilitiesChangeEvent();
        }

        public void AddAbility(Ability ability)
        {
            if (_abilities.Count == 0)
            {
                GameObject abilities = new GameObject("Abilities");
                abilities.transform.parent = gameObject.transform;
            }
            Transform parent = _abilities[0].gameObject.transform.parent;
            ability.gameObject.transform.parent = parent;
            UpdateAbilityList();
        }
    }
}