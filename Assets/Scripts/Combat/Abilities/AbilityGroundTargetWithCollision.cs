using System.Collections.Generic;
using UnityEngine;

namespace Demo.Abilities
{
    public class AbilityGroundTargetWithCollision : AbilityGroundTarget
    {
        new Rigidbody rigidbody;
        List<Collider> currentCollisions = new List<Collider>();
        void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }
        public override void SetPosition(Vector3 point)
        {
            rigidbody.MovePosition(point);
        }

        public override bool IsPositionValid()
        {
            return currentCollisions.Count == 0;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Terrain")
            {
                currentCollisions.Add(other);
            }
        }

        void OnTriggerExit(Collider other)
        {
            currentCollisions.Remove(other);
        }
    }
}