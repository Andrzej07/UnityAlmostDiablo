
using System;

namespace Demo.Combat
{
    public interface IHealable
    {
        event Action<Healing> ReceiveHealingEvent;
        void ReceiveHealing(Healing healing);
    }
}