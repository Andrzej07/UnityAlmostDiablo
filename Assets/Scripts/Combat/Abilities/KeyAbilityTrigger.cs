using UnityEngine;

namespace Demo.Abilities
{
    public class KeyAbilityTrigger : MonoBehaviour, IAbilityTrigger
    {
        public KeyCode key;

        public event AbilityTriggerDelegate AbilityTriggerEvent;

        void Update()
        {
            if (Input.GetKeyDown(key))
            {
                if (AbilityTriggerEvent != null)
                    AbilityTriggerEvent();
            }
        }
    }
}