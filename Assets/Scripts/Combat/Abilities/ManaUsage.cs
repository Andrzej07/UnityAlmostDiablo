using Demo.Characters;
using UnityEngine;

namespace Demo.Abilities
{
    [RequireComponent(typeof(Ability))]
    public class ManaUsage : MonoBehaviour, ICastRequirement, IPostCastAction, ICastErrorAnnouncer, ITooltipPart
    {
        public float manaCost;
        Ability ability;

        public int Order
        {
            get
            {
                return 1;
            }
        }

        public string TooltipPart
        {
            get
            {
                return string.Format("Mana: {0}", manaCost);
            }
        }

        public event CastErrorDelegate CastErrorEvent;

        void Awake()
        {
            ability = GetComponent<Ability>();
        }

        public bool IsSatisfied()
        {
            Mana mana = GetMana();
            if (mana.CanAfford(manaCost))
            {
                return true;
            }
            else
            {
                if (CastErrorEvent != null)
                {
                    CastErrorEvent("Not enough mana");
                }
                return false;
            }
        }

        public void Perform()
        {
            Mana mana = GetMana();
            mana.Use(manaCost);
        }

        Mana GetMana()
        {
            ICombatStatistics stats = ability.source.GetComponent<ICombatStatistics>();
            if (stats == null)
                Debug.LogWarning("Casting ability by entity with no ICombatStatistics component");
            return stats.Mana;
        }
    }
}