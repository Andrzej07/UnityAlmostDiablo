using System;

namespace Demo.Abilities
{
    public interface IRequirementSatisfier
    {
        ICastRequirement requirement { get; }
        event Action RequirementSatisfiedEvent;
        void Satisfy();
    }
}