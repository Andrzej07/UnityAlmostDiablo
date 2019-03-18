using UnityEngine;

namespace Demo.Abilities
{
    public class InvokeEffectsAbility : Ability
    {
        public Effect[] effects;
        GameObject currentTarget;
        new void Awake()
        {
            base.Awake();
            ITargetingSingle targeting = GetComponent<ITargetingSingle>();
            targeting.TargetAcquiredEvent += OnTargetAcquiredEvent;
        }
        protected override void CastAbility()
        {
            foreach (Effect effect in effects)
            {
                effect.ApplyEffect(source, currentTarget);
            }
        }

        void OnTargetAcquiredEvent(GameObject target)
        {
            currentTarget = target;
        }
    }
}