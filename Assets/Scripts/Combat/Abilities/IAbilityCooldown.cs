using System;

namespace Demo.Abilities
{
    public interface IAbilityCooldown
    {
        event Action<float> RemaingingCooldownChangedEvent;
        float RemainingCooldown { get; }
    }
}