using System;
using Demo.Characters;
using UnityEngine;

namespace Demo.Abilities
{
    [RequireComponent(typeof(Ability))]
    public class MoveInRange : MonoBehaviour, IRequirementSatisfier
    {
        public ICastRequirement requirement
        {
            get
            {
                if (rangeRequirement == null)
                    rangeRequirement = GetComponent<RangeRequirement>();
                return rangeRequirement;
            }
        }

        public event Action RequirementSatisfiedEvent;

        ICastRequirement rangeRequirement;
        ICharacterMovement characterMovement;
        GameObject currentTarget;
        bool working = false;

        void Awake()
        {

            ITargetingSingle targeting = GetComponent<ITargetingSingle>();
            targeting.TargetAcquiredEvent += OnTargetAcquiredEvent;
        }

        void Start()
        {
            Ability ability = GetComponent<Ability>();
            characterMovement = ability.source.GetComponent<ICharacterMovement>();
            if (characterMovement == null)
                Debug.LogWarning("MoveInRange: ability source has no ICharacterMovement assigned");
        }

        public void Satisfy()
        {
            working = true;
        }

        void Update()
        {
            if (working)
            {
                if (rangeRequirement.IsSatisfied())
                {
                    working = false;
                    characterMovement.Stop();
                    if (RequirementSatisfiedEvent != null)
                        RequirementSatisfiedEvent();
                }
                else
                {
                    characterMovement.MoveTo(currentTarget.transform.position);
                }
            }
        }

        void OnTargetAcquiredEvent(GameObject target)
        {
            currentTarget = target;
        }
    }
}