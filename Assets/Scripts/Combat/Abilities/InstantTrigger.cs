using System.Collections;
using UnityEngine;

namespace Demo.Abilities
{
    public class InstantTrigger : MonoBehaviour, IAbilityTrigger
    {
        public event AbilityTriggerDelegate AbilityTriggerEvent;

        bool triggered = false;
        void OnEnable()
        {
            triggered = false;
        }

        void LateUpdate()
        {
            if (!triggered)
            {
                StartCoroutine(DelayedTrigger());
            }
        }

        void Trigger()
        {
            if (AbilityTriggerEvent != null)
                AbilityTriggerEvent();
            else
                Debug.LogWarning("AbilityTriggerEvent is null!");
        }

        IEnumerator DelayedTrigger()
        {
            yield return null;
            triggered = true;
            Trigger();
        }
    }
}