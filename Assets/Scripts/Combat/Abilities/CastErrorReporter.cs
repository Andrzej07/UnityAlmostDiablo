using UnityEngine;

namespace Demo.Abilities
{
    public class CastErrorReporter : MonoBehaviour
    {
        IErrorReporter errorReporter;

        void Awake()
        {
            ICastErrorAnnouncer[] announcers = GetComponents<ICastErrorAnnouncer>();
            foreach (ICastErrorAnnouncer announcer in announcers)
            {
                announcer.CastErrorEvent += OnCastErrorEvent;
            }
        }

        void Start()
        {
            Ability ability = GetComponent<Ability>();
            errorReporter = ability.source.GetComponent<IErrorReporter>();
        }

        void OnCastErrorEvent(string msg)
        {
            errorReporter.ReportError(msg);
        }
    }
}