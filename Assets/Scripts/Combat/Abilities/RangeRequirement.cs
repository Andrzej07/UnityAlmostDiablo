using UnityEngine;
using UnityEngine.AI;

namespace Demo.Abilities
{
    [RequireComponent(typeof(Ability))]
    public class RangeRequirement : MonoBehaviour, ICastRequirement, ITooltipPart
    {
        public float range;
        Ability ability;
        GameObject currentTarget;

        public int Order
        {
            get
            {
                return 10;
            }
        }

        public string TooltipPart
        {
            get
            {
                return string.Format("Range: {0}", range);
            }
        }

        void Awake()
        {
            ability = GetComponent<Ability>();
            ITargetingSingle targeting = GetComponent<ITargetingSingle>();
            targeting.TargetAcquiredEvent += OnTargetAcquiredEvent;
        }
        public bool IsSatisfied()
        {
            if (currentTarget == null)
                return true;
            Vector3 sourcePos = ability.source.transform.position;
            Vector3 targetPos = currentTarget.transform.position;
            float distance = Vector3.Distance(sourcePos, targetPos);
            return distance <= range;
        }

        void OnTargetAcquiredEvent(GameObject target)
        {
            currentTarget = target;
        }

    }
}