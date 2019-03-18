using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Abilities
{
    public class RotateTowardsTarget : MonoBehaviour, IPostCastAction
    {
        GameObject currentTarget;
        Ability ability;

        public void Perform()
        {
            Vector3 targetPostition = new Vector3(currentTarget.transform.position.x,
                                                   ability.source.transform.position.y,
                                                   currentTarget.transform.position.z);
            ability.source.transform.LookAt(targetPostition);
        }

        void Awake()
        {
            ITargetingSingle targeting = GetComponent<ITargetingSingle>();
            targeting.TargetAcquiredEvent += OnTargetAcquiredEvent;
            ability = GetComponent<Ability>();
        }

        void OnTargetAcquiredEvent(GameObject target)
        {
            currentTarget = target;
        }
    }
}