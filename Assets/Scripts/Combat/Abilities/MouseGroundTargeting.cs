
using UnityEngine;

namespace Demo.Abilities
{
    public class MouseGroundTargeting : MonoBehaviour, ITargetingSingle
    {
        public event TargetAcquiredDelegate TargetAcquiredEvent;
        public LayerMask raycastLayer;
        public AbilityGroundTarget groundMarker;

        void OnEnable()
        {
            groundMarker.gameObject.SetActive(true);
        }

        void Start()
        {
            Setup();
        }

        void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool raycastHit = Physics.Raycast(ray, out hit, raycastLayer);
            if (raycastHit)
            {
                //Debug.Log("Setting position to " + hit.point);
                groundMarker.SetPosition(hit.point);
            }
        }

        void Setup()
        {
            if (TargetAcquiredEvent != null)
                TargetAcquiredEvent(groundMarker.gameObject);
            else
                Debug.LogWarning(gameObject.name + " MouseGroundTargeting: Target acquisition event is null");
        }

        void OnDisable()
        {
            groundMarker.gameObject.SetActive(false);
        }
    }
}