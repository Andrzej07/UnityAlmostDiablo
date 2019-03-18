
using System;
using UnityEngine;

namespace Demo.Characters
{
    [Serializable]
    public class Health
    {
        public event Action HealthDepletedEvent;
        [SerializeField]
        int _maxHealth;
        [SerializeField]
        int _health;
        public int CurrentHealth
        {
            get
            {
                return _health;
            }
            set
            {
                _health = value;
            }
        }

        public int MaxHealth
        {
            get
            {
                return _maxHealth;
            }
            set
            {
                _maxHealth = value;
            }
        }

        public void DealDamage(int amount)
        {
            _health -= amount;
            if (_health <= Mathf.Epsilon)
            {
                _health = 0;
                if (HealthDepletedEvent != null)
                    HealthDepletedEvent();
            }
        }

        public int RestoreHealth(int amount)
        {
            int value = Mathf.RoundToInt(Mathf.Min(amount, _maxHealth - _health));
            _health += value;
            return value;
        }

        public void IncreaseMaxAndCurrent(int amount)
        {
            _maxHealth += amount;
            _health += amount;
        }

    }
}