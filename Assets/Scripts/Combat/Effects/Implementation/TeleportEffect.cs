using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using Demo.Characters;

[CreateAssetMenu(menuName = "Effect/Teleport")]
public class TeleportEffect : Effect
{
    public override void ApplyEffect(GameObject source, GameObject target)
    {
        Vector3 newPosition = target.transform.position;
        ICharacterMovement movement = source.GetComponent<ICharacterMovement>();
        if (movement != null)
        {
            movement.Teleport(newPosition);
        }
        else
        {
            source.transform.position = newPosition;
        }
    }
}
