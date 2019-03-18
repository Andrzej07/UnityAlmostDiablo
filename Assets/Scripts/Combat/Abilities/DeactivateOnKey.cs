using UnityEngine;

namespace Demo.Abilities
{
    public class DeactivateOnKey : MonoBehaviour
    {
        public KeyCode key;

        void Update()
        {
            if (Input.GetKeyDown(key))
            {
                gameObject.SetActive(false);
            }
        }
    }

}