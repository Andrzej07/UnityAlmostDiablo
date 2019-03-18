using UnityEngine;

namespace Demo.Abilities
{
    public abstract class AbilityGroundTarget : MonoBehaviour
    {
        public abstract void SetPosition(Vector3 point);
        public abstract bool IsPositionValid();
    }
}