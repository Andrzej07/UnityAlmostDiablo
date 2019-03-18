using System.Collections;
using System.Collections.Generic;
using Demo.Combat;
using UnityEngine;

namespace Demo.Characters
{
    public class KillExperienceReward : MonoBehaviour, IExperienceReward
    {
        [SerializeField]
        int _experienceAmount;
        public int ExperienceAmount
        {
            get
            {
                return _experienceAmount;
            }
            set
            {
                _experienceAmount = value;
            }
        }

        List<IExperienceCollector> experienceCollectors = new List<IExperienceCollector>();

        void Awake()
        {
            IDamageable damageable = GetComponent<IDamageable>();
            damageable.ReceiveDamageEvent += OnReceiveDamageEvent;
            damageable.DeathEvent += OnDeathEvent;
        }

        void OnReceiveDamageEvent(Damage damage)
        {
            IExperienceCollector collector = damage.source.GetComponent<IExperienceCollector>();
            if (collector != null && !experienceCollectors.Contains(collector))
            {
                experienceCollectors.Add(collector);
            }
        }

        void OnDeathEvent(GameObject gameObject)
        {
            foreach (IExperienceCollector collector in experienceCollectors)
            {
                collector.GainExperience(ExperienceAmount);
            }
        }
    }
}