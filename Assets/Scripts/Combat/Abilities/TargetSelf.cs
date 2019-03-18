using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Abilities
{
    public class TargetSelf : MonoBehaviour, ITargetingSingle
    {
        public event TargetAcquiredDelegate TargetAcquiredEvent;

        void Start()
        {
            if (TargetAcquiredEvent != null)
            {
                Ability ability = GetComponent<Ability>();
                TargetAcquiredEvent(ability.source);
            }
            else
            {
                Debug.LogWarningFormat("{0} : TargetSelf TargetAcquiredEvent is null", gameObject.name);
            }
        }

    }
}