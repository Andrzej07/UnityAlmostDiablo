using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Demo.Abilities
{
    public abstract class Ability : MonoBehaviour
    {
        GameObject _source;
        public GameObject source
        {
            get
            {
                if (_source == null)
                {
                    _source = GetComponentInParent<AbilitiesController>().gameObject;
                }
                return _source;
            }
        }
        public string abilityName;
        [TextArea(3, 10)]
        public string abilityTooltip;

        string fullTooltip;
        public string Tooltip
        {
            get
            {
                if (string.IsNullOrEmpty(fullTooltip))
                {
                    BuildTooltip();
                }
                return fullTooltip;
            }
        }
        public event Action DeactivatedEvent;

        protected ICastRequirement[] _requirements;
        protected ICastRequirement[] requirements
        {
            get
            {
                if (_requirements == null)
                {
                    _requirements = GetComponents<ICastRequirement>();
                }
                return _requirements;
            }
        }
        protected IPostCastAction[] actions;

        protected Dictionary<ICastRequirement, IRequirementSatisfier> _satisfiers;
        protected Dictionary<ICastRequirement, IRequirementSatisfier> satisfiers
        {
            get
            {
                if (_satisfiers == null)
                {
                    _satisfiers = new Dictionary<ICastRequirement, IRequirementSatisfier>();
                    IRequirementSatisfier[] satisfiers = GetComponents<IRequirementSatisfier>();
                    foreach (IRequirementSatisfier satisfier in satisfiers)
                    {
                        _satisfiers.Add(satisfier.requirement, satisfier);
                    }
                }
                return _satisfiers;
            }
        }

        protected void Awake()
        {
            IAbilityTrigger[] triggers = GetComponents<IAbilityTrigger>();
            foreach (IAbilityTrigger trigger in triggers)
            {
                trigger.AbilityTriggerEvent += CheckAndCast;
            }
            actions = GetComponents<IPostCastAction>();
        }

        void CheckAndCast()
        {
            foreach (ICastRequirement requirement in requirements)
            {
                if (!requirement.IsSatisfied())
                {
                    if (satisfiers.ContainsKey(requirement))
                    {
                        Action onRequirementSatisfiedEvent = null;
                        onRequirementSatisfiedEvent = () =>
                        {
                            satisfiers[requirement].RequirementSatisfiedEvent -= onRequirementSatisfiedEvent;
                            CheckAndCast();
                        };
                        satisfiers[requirement].RequirementSatisfiedEvent += onRequirementSatisfiedEvent;
                        satisfiers[requirement].Satisfy();
                    }
                    return;
                }
            }
            CastAbility();
            foreach (IPostCastAction action in actions)
            {
                action.Perform();
            }
        }

        void OnDisable()
        {
            if (DeactivatedEvent != null)
            {
                DeactivatedEvent();
            }
        }

        public bool CheckRequirements()
        {
            foreach (ICastRequirement requirement in requirements)
            {
                if (!satisfiers.ContainsKey(requirement) && !requirement.IsSatisfied())
                {
                    return false;
                }
            }
            return true;
        }


        void BuildTooltip()
        {
            ITooltipPart[] parts = GetComponents<ITooltipPart>();
            Array.Sort(parts, (a, b) => a.Order.CompareTo(b.Order));
            fullTooltip = "";
            foreach (ITooltipPart part in parts)
            {
                fullTooltip += String.Format("{0}\n", part.TooltipPart);
            }
            fullTooltip += abilityTooltip;
        }

        protected abstract void CastAbility();
    }
}