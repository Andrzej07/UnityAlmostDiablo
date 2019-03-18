using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Abilities
{
    public class ShootProjectileAbility : Ability
    {
        public ProjectileController projectilePrefab;
        public Transform origin;
        public float velocity;
        public float timeToLive;

        Transform projectiles;
        GameObject currentTarget;

        new void Awake()
        {
            base.Awake();
            ITargetingSingle targeting = GetComponent<ITargetingSingle>();
            targeting.TargetAcquiredEvent += OnTargetAcquiredEvent;
        }
        void Start()
        {
            projectiles = GameObject.Find("Projectiles").transform;
            if (origin == null)
            {
                origin = source.transform.Find("ProjectileOrigin");
            }
        }

        protected override void CastAbility()
        {
            Vector3 targetPosition = currentTarget.transform.position;
            Debug.Log("Shoot projectile: " + targetPosition + " " + currentTarget.name);
            ProjectileController projectileInstance = Instantiate(projectilePrefab.gameObject, projectiles).GetComponent<ProjectileController>();
            Vector3 dir = (targetPosition - origin.position);
            dir.y = 0;
            dir.Normalize();
            projectileInstance.transform.position = origin.position + dir;
            projectileInstance.direction = dir;
            projectileInstance.timeToLive = timeToLive;
            projectileInstance.velocity = velocity;
            projectileInstance.source = source;
        }

        void OnTargetAcquiredEvent(GameObject target)
        {
            currentTarget = target;
        }
    }
}