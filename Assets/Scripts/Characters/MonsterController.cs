using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Demo.Abilities;
using Demo.Combat;

namespace Demo.Characters
{
    public class MonsterController : CharacterController
    {
        [Header("Monster controller")]
        public Transform player;
        public string monsterName = "Monster";

        new void Awake()
        {
            base.Awake();
            IDamageable damageable = GetComponent<IDamageable>();
            damageable.DeathEvent += OnDeath;
        }

        private void OnDeath(GameObject gameObject)
        {
            GetComponent<Collider>().enabled = false;
            agent.enabled = false;
            agent.velocity = Vector3.zero;
            StartCoroutine(DestroyAfterDelay(5));
        }

        private IEnumerator DestroyAfterDelay(int delay)
        {
            yield return new WaitForSeconds(delay);
            ObjectDestructionController.Destroy2(gameObject);
        }
    }
}