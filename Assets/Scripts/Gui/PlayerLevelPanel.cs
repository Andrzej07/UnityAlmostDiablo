using System.Collections;
using System.Collections.Generic;
using Demo.Characters;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelPanel : MonoBehaviour
{

    public GameObject labelPrefab;
    public PlayerLevelController playerLevelController;

    private Text playerLevel;
    private Text currentExperience;
    private Text totalExperience;

    // Use this for initialization
    void Start()
    {
        playerLevel = Instantiate(labelPrefab, gameObject.transform).GetComponent<Text>();
        currentExperience = Instantiate(labelPrefab, gameObject.transform).GetComponent<Text>();
        totalExperience = Instantiate(labelPrefab, gameObject.transform).GetComponent<Text>();
        playerLevelController.playerLevelChangeDelegate += OnLevelChange;
        playerLevelController.experienceGainedDelegate += OnExperienceGained;
        OnExperienceGained();
        OnLevelChange();
    }



    void OnExperienceGained()
    {
        currentExperience.text = "Experience until level: " + (playerLevelController.currentExperienceRequired - playerLevelController.accumulatedExperience);
        totalExperience.text = "Total experience: " + playerLevelController.totalAccumulatedExperience;
    }

    void OnLevelChange()
    {
        playerLevel.text = "Player level: " + playerLevelController.level;
        OnExperienceGained();
    }
}
