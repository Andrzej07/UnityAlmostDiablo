
using System;
using UnityEngine;

namespace Demo.Characters
{
    [Serializable]
    public class Mana
    {
        [SerializeField]
        float _maxMana;
        [SerializeField]
        float _currentMana;
        [SerializeField]
        float _regenerationRate;

        public float MaxMana
        {
            get
            {
                return _maxMana;
            }

            set
            {
                _maxMana = value;
            }
        }

        public float RegenerationRate
        {
            get
            {
                return _regenerationRate;
            }

            set
            {
                _regenerationRate = value;
            }
        }

        public float CurrentMana
        {
            get
            {
                return _currentMana;
            }

            set
            {
                _currentMana = value;
            }
        }

        public void Update(float time)
        {
            CurrentMana += RegenerationRate * time;
            if (CurrentMana > MaxMana)
                CurrentMana = MaxMana;
        }

        public void Use(float amount)
        {
            CurrentMana -= amount;
        }

        public bool CanAfford(float amount)
        {
            return CurrentMana >= amount;
        }

        public void IncreaseMaxAndCurrent(float amount)
        {
            CurrentMana += amount;
            MaxMana += amount;
        }
    }
}