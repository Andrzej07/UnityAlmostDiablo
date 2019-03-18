
using UnityEngine;

namespace Demo.Abilities
{
    public class AbilityGroundTargetEmpty : AbilityGroundTarget
    {
        public override bool IsPositionValid()
        {
            return true;
        }

        public override void SetPosition(Vector3 point)
        {
            transform.position = point;
        }
    }
}