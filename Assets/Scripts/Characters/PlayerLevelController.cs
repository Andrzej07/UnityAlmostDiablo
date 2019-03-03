using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelController : MonoBehaviour
{

    [HideInInspector]
    public int level = 1;
    [HideInInspector]
    public int currentExperienceRequired = 100;
    public int[] experienceRequired = new int[] { 100 };
    public int accumulatedExperience = 0;
    [HideInInspector]
    public int totalAccumulatedExperience = 0;

    public delegate void OnPlayerLevelChangeDelegate();
    public event OnPlayerLevelChangeDelegate playerLevelChangeDelegate;

    public delegate void OnExperienceGainedDelegate();
    public event OnExperienceGainedDelegate experienceGainedDelegate;

    private CharacterStatistics characterStatistics;
    private GameController gameController;
    private GuiController guiController;

    private int availableStatPoints = 0;

    void Awake()
    {
        totalAccumulatedExperience = accumulatedExperience;
    }
    // Use this for initialization
    void Start()
    {
        gameController = GameObject.Find("Game").GetComponent<GameController>();
        gameController.enemyDeathDelegate += OnEnemyDeath;
        guiController = GameObject.Find("GUI").GetComponent<GuiController>();
        characterStatistics = gameObject.GetComponent<CharacterStatistics>();

    }

    // Update is called once per frame
    void Update()
    {

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

    void OnEnemyDeath(GameObject enemy)
    {
        MonsterController monsterController = enemy.GetComponent<MonsterController>();
        GainExperience(monsterController.experienceReward);
    }


    public void StatlUpBonus(int x)
    {
        if (availableStatPoints <= 0)
            return;
        switch (x)
        {
            case 0:
                characterStatistics.maxHealth += 10;
                characterStatistics.health += 10;
                break;
            case 1:
                characterStatistics.maxMana += 10;
                characterStatistics.mana += 10;
                break;
            case 2:
                characterStatistics.strength += 1;
                break;
            case 3:
                characterStatistics.intelligence += 1;
                break;
        }
        availableStatPoints--;
        if (availableStatPoints == 0)
        {
            guiController.HideStatUpPanel();
        }
    }
}
