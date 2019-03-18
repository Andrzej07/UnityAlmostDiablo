using UnityEngine;
using UnityEngine.AI;

namespace Demo.Abilities
{
    [RequireComponent(typeof(Ability))]
    public class LineOfSightRequirement : MonoBehaviour, ICastRequirement
    {
        Ability ability;
        GameObject currentTarget;
        void Awake()
        {
            ability = GetComponent<Ability>();
            ITargetingSingle targeting = GetComponent<ITargetingSingle>();
            targeting.TargetAcquiredEvent += OnTargetAcquiredEvent;
        }
        public bool IsSatisfied()
        {
            GameObject source = ability.source;
            GameObject target = currentTarget;
            RaycastHit terrainHit;
            Physics.Linecast(source.transform.position, target.transform.position, out terrainHit, 1 << LayerMask.NameToLayer("Terrain"));
            int previousLayer = target.layer;
            int targetLayer = LayerMask.NameToLayer("Target");
            target.layer = targetLayer;
            RaycastHit targetHit;
            Physics.Linecast(source.transform.position, target.transform.position, out targetHit, 1 << targetLayer);
            target.layer = previousLayer;

            float terrainDistance = Vector3.Distance(source.transform.position, terrainHit.point);
            float targetDistance = Vector3.Distance(source.transform.position, targetHit.point);
            return targetDistance < terrainDistance;
        }

        void OnTargetAcquiredEvent(GameObject target)
        {
            currentTarget = target;
        }
        /* public bool SatisfyRequirement()
        {
            NavMeshAgent agent = source.GetComponent<NavMeshAgent>();
            agent.destination = target.transform.position;
            agent.stoppingDistance = 0;
            return true;
        } */
    }
}