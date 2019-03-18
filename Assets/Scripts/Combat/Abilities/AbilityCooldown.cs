using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.Abilities
{
    [RequireComponent(typeof(Ability))]
    public class AbilityCooldown : MonoBehaviour, IPostCastAction, ICastRequirement, ICastErrorAnnouncer, IAbilityCooldown
    {
        [SerializeField]
        float cooldown;
        float allowedCastTime;

        public event CastErrorDelegate CastErrorEvent;
        public event Action<float> RemaingingCooldownChangedEvent;

        public int Order
        {
            get
            {
                return 5;
            }
        }

        public string TooltipPart
        {
            get
            {
                return string.Format("Cooldown: {0}s", cooldown);
            }
        }

        public float RemainingCooldown
        {
            get
            {
                return Mathf.Max(0, allowedCastTime - Time.time);
            }
        }

        void Awake()
        {
            allowedCastTime = -1;
        }

        public bool IsSatisfied()
        {
            if (Time.time > allowedCastTime)
            {
                return true;
            }
            else
            {
                if (CastErrorEvent != null)
                {
                    CastErrorEvent("Ability isn't ready yet");
                }
                return false;
            }
        }

        public void Perform()
        {
            allowedCastTime = Time.time + cooldown;
            if (RemaingingCooldownChangedEvent != null)
                GameController.instance.StartCoroutine(Countdown());
        }

        IEnumerator Countdown()
        {
            float time = cooldown;
            float timeStep = 0.1f;
            for (float i = time; i > float.Epsilon; i -= timeStep)
            {
                RemaingingCooldownChangedEvent(i);
                yield return new WaitForSeconds(timeStep);
            }
            RemaingingCooldownChangedEvent(0);
        }
    }
}