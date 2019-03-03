using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

[CreateAssetMenu(menuName = "Effect/Teleport")]
public class TeleportEffect : Effect
{
    public override void ApplyEffect(GameObject source, GameObject target)
    {
        Vector3 newPosition = target.transform.position;
        Debug.Log(newPosition);
        NavMeshAgent agent = source.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.destination = newPosition;
            agent.Warp(newPosition);
        }
        else
        {
            source.transform.position = newPosition;
        }
    }
}
