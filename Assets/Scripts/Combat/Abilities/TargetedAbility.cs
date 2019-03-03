using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Ability/Target")]
public class TargetedAbility : Ability
{
    public enum TargetType
    {
        SELF,
        ENEMY,
        GROUND
    }

    public TargetType targetType;
    public float range;
    public bool requireLineOfSight;

    protected override bool TriggerEffect(GameObject source, GameObject target)
    {
        if (targetType == TargetType.SELF)
            target = source;
        if (target == null)
        {
            return false;
        }
        NavMeshAgent agent = source.GetComponent<NavMeshAgent>();
        if (agent != null && agent.enabled)
        {
            agent.destination = source.transform.position;
        }
        Vector3 targetPostition = new Vector3(target.transform.position.x,
                                       source.transform.position.y,
                                       target.transform.position.z);
        source.transform.LookAt(targetPostition);

        foreach (Effect effect in effects)
        {
            effect.ApplyEffect(source, target);
        }
        return true;
    }

    protected override void GetInPosition(GameObject source, GameObject target)
    {
        if (targetType == TargetType.SELF)
            return;
        NavMeshAgent agent = source.GetComponent<NavMeshAgent>();
        if (agent == null)
            return;
        agent.isStopped = false;
        if (IsInRange(source.transform, target.transform))
        {
            if (!IsInLineOfSight(source, target))
            {
                agent.destination = target.transform.position;
                agent.stoppingDistance = 0;
            }
        }
        else
        {
            agent.destination = target.transform.position;
            agent.stoppingDistance = range;
        }
    }

    public override bool IsInPosition(GameObject source, GameObject target)
    {
        return targetType == TargetType.SELF || (IsInRange(source.transform, target.transform) && (!requireLineOfSight || IsInLineOfSight(source, target)));
    }

    bool IsInRange(Transform source, Transform target)
    {
        float distance = Vector3.Distance(source.position, target.position);
        return distance <= range;
    }

    protected bool IsInLineOfSight(GameObject source, GameObject target)
    {
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
}
