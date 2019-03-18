using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfoPanel : MonoBehaviour
{
    public GameObject labelPrefab;


    private GameController gameController;
    private Text waveNumber;
    private Text remainingEnemies;

    void Start()
    {
        gameController = GameController.instance;

        waveNumber = Instantiate(labelPrefab, gameObject.transform).GetComponent<Text>();
        remainingEnemies = Instantiate(labelPrefab, gameObject.transform).GetComponent<Text>();
        gameController.waveChangeEvent += OnWaveChange;
        gameController.EnemyCountChangedEvent += OnEnemyCountChange;
        OnWaveChange();
    }



    void OnEnemyCountChange(int count)
    {
        remainingEnemies.text = "Enemies remaining: " + count;
    }

    void OnWaveChange()
    {
        waveNumber.text = "Wave: " + gameController.waveNumber;
    }
}
