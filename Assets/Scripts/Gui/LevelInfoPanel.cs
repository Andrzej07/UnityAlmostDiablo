using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfoPanel : MonoBehaviour {
    public GameObject labelPrefab;


    private GameController gameController;
    private Text waveNumber;
    private Text remainingEnemies;

    // Use this for initialization
    void Start () {
        gameController = GameObject.Find("Game").GetComponent<GameController>();

        waveNumber = Instantiate(labelPrefab, gameObject.transform).GetComponent<Text>();
        remainingEnemies = Instantiate(labelPrefab, gameObject.transform).GetComponent<Text>();
        gameController.waveChangeDelegate += OnWaveChange;
        gameController.enemyCountChanged += OnEnemyCountChange;
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
