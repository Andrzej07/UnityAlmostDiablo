using UnityEngine;

namespace Demo.Abilities
{
    [RequireComponent(typeof(IAbilityTrigger))]
    public class DeactivateOnTrigger : MonoBehaviour
    {
        void Awake()
        {
            IAbilityTrigger[] triggers = GetComponents<IAbilityTrigger>();
            foreach (IAbilityTrigger trigger in triggers)
            {
                trigger.AbilityTriggerEvent += OnAbilityTriggerEvent;
            }
        }

        void OnAbilityTriggerEvent()
        {
            gameObject.SetActive(false);
        }
    }
}