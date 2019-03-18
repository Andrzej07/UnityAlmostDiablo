namespace Demo.Abilities
{
    public delegate void CastErrorDelegate(string msg);
    public interface ICastErrorAnnouncer
    {
        event CastErrorDelegate CastErrorEvent;
    }
}