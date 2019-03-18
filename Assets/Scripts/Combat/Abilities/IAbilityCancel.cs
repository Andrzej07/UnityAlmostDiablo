namespace Demo.Abilities
{
    public delegate void TargetingCancelled();
    public interface IAbilityCancel
    {
        event TargetingCancelled TargetingCancelledEvent;
    }
}