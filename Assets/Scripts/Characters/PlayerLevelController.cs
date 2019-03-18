using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Characters
{
    public class PlayerLevelController : MonoBehaviour, IExperienceCollector
    {

        [HideInInspector]
        public int level = 1;
        [HideInInspector]
        public int currentExperienceRequired = 100;
        public int[] experienceRequired = new int[] { 100 };
        public int accumulatedExperience = 0;
        [HideInInspector]
        public int totalAccumulatedExperience = 0;

        public event Action playerLevelChangeDelegate;
        public event Action experienceGainedDelegate;

        private ICombatStatistics characterStatistics;
        private GameController gameController;
        private GuiController guiController;

        private int availableStatPoints = 0;

        void Awake()
        {
            totalAccumulatedExperience = accumulatedExperience;
        }

        void Start()
        {
            gameController = GameController.instance;
            guiController = gameController.guiController;
            characterStatistics = gameObject.GetComponent<ICombatStatistics>();
        }

        void SetRequiredExperience()
        {
            if (level <= experienceRequired.Length)
            {
                currentExperienceRequired = experienceRequired[level - 1];
            }
            else
            {
                currentExperienceRequired = Mathf.RoundToInt(experienceRequired[experienceRequired.Length - 1] * (1 + 0.1f * (level - experienceRequired.Length)));
            }
        }

        public void GainExperience(int amount)
        {
            accumulatedExperience += amount;
            totalAccumulatedExperience += amount;
            while (accumulatedExperience > currentExperienceRequired)
            {
                GainLevel();
            }
            if (experienceGainedDelegate != null)
                experienceGainedDelegate();
        }

        private void GainLevel()
        {
            accumulatedExperience -= currentExperienceRequired;
            level++;
            SetRequiredExperience();
            if (playerLevelChangeDelegate != null)
                playerLevelChangeDelegate();
            guiController.ShowStatUpPanel();
            availableStatPoints++;
        }

        public void StatlUpBonus(int x)
        {
            if (availableStatPoints <= 0)
                return;
            switch (x)
            {
                case 0:
                    characterStatistics.Health.IncreaseMaxAndCurrent(10);
                    break;
                case 1:
                    characterStatistics.Mana.IncreaseMaxAndCurrent(10);
                    break;
                case 2:
                    characterStatistics.Strength += 1;
                    break;
                case 3:
                    characterStatistics.Intelligence += 1;
                    break;
            }
            availableStatPoints--;
            if (availableStatPoints == 0)
            {
                guiController.HideStatUpPanel();
            }
        }
    }
}