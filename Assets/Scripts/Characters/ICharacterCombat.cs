using UnityEngine;

namespace Demo.Characters
{
    public interface ICharacterCombat
    {
        void ActivateAbility(int index);
        void Attack(GameObject target);
    }
}