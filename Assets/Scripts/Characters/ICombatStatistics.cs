
using System;

namespace Demo.Characters
{
    public interface ICombatStatistics
    {
        event Action StatsChangeEvent;
        Mana Mana { get; }
        Health Health { get; }
        int Armor { get; set; }
        int Strength { get; set; }
        int Intelligence { get; set; }
    }
}