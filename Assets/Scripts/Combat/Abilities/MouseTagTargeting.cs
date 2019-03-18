
using UnityEngine;

namespace Demo.Abilities
{
    public class MouseTagTargeting : MonoBehaviour, ITargetingSingle
    {
        public event TargetAcquiredDelegate TargetAcquiredEvent;
        public string targetTag;

        void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool raycastHit = Physics.Raycast(ray, out hit);
            if (raycastHit)
            {
                if (hit.transform.gameObject.tag == targetTag)
                {
                    if (TargetAcquiredEvent != null)
                        TargetAcquiredEvent(hit.transform.gameObject);
                    else
                        Debug.LogWarning("Target acquisition event is null");
                }
            }
        }
    }
}