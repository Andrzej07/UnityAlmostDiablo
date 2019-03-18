using UnityEngine;

namespace Demo.Abilities
{
    public delegate void TargetsAcquiredDelegate(GameObject[] targets);
    public interface ITargetingMultiple
    {
        event TargetsAcquiredDelegate TargetsAcquiredEvent;
    }
}