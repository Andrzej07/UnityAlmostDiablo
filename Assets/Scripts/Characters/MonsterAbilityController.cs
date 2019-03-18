using System.Collections;
using System.Collections.Generic;
using Demo.Abilities;
using Demo.Combat;
using UnityEngine;

namespace Demo.Characters
{
    public class MonsterAbilityController : MonoBehaviour, ITargetingSingle
    {
        public event TargetAcquiredDelegate TargetAcquiredEvent;

        GameObject target;

        void Awake()
        {
            ICastErrorAnnouncer[] announcers = GetComponents<ICastErrorAnnouncer>();
            foreach (ICastErrorAnnouncer announcer in announcers)
            {
                announcer.CastErrorEvent += OnCastErrorEvent;
            }
        }

        void Start()
        {
            GameObject source = GetComponent<Ability>().source;
            target = source.GetComponent<MonsterController>().player.gameObject;
            if (TargetAcquiredEvent != null)
            {
                TargetAcquiredEvent(target);
            }
            source.GetComponent<IDamageable>().DeathEvent += OnDeathEvent;
        }

        void OnCastErrorEvent(string msg)
        {
            gameObject.SetActive(false);
        }

        void OnDeathEvent(GameObject ded)
        {
            gameObject.SetActive(false);
        }
    }
}