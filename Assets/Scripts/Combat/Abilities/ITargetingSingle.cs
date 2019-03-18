using UnityEngine;

namespace Demo.Abilities
{
    public delegate void TargetAcquiredDelegate(GameObject target);
    public interface ITargetingSingle
    {
        event TargetAcquiredDelegate TargetAcquiredEvent;
    }
}