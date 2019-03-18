namespace Demo.Abilities
{
    public delegate void AbilityTriggerDelegate();
    public interface IAbilityTrigger
    {
        event AbilityTriggerDelegate AbilityTriggerEvent;
    }
}