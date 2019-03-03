using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public static GameController instance;

    public delegate void OnWaveChangeDelegate();
    public event OnWaveChangeDelegate waveChangeDelegate;

    public delegate void OnEnemyCountChanged(int count);
    public event OnEnemyCountChanged enemyCountChanged;

    public delegate void OnEnemySpawned();
    public OnEnemySpawned enemySpawnedDelegate;

    public delegate void OnEnemyDeath(GameObject enemy);
    public OnEnemyDeath enemyDeathDelegate;

    public MonsterSpawnerController monsterSpawnerController;
    [HideInInspector]
    public int waveNumber;

    private Transform enemies;
    [HideInInspector]
    public int enemyCount;    

    public bool AreHostile(GameObject obj1, GameObject obj2)
    {
        return (obj1.tag == "Player" && obj2.tag == "Enemy") || (obj1.tag == "Enemy" && obj2.tag == "Player");
    }

    private void Awake()
    {
        instance = this;
        enemySpawnedDelegate += delegate ()
        {
            enemyCount++;
            if (enemyCountChanged != null)
                enemyCountChanged(enemyCount);
        };
        enemyDeathDelegate += delegate (GameObject enemy)
        {
            enemyCount--;
            if (enemyCount == 0 && monsterSpawnerController.waitingToSpawn == 0)
            {
                NewWave();
            }
            if (enemyCountChanged != null)
                enemyCountChanged(enemyCount);
        };
    }

    // Use this for initialization
    void Start () {
        SpawnWave();
        enemies = GameObject.Find("Enemies").transform;
        enemyCount = enemies.childCount;
        if(enemyCountChanged != null)
            enemyCountChanged(enemyCount);
	}
	
	// Update is called once per frame
	void Update () {

    }

    void SpawnWave()
    {
        monsterSpawnerController.waitingToSpawn = waveNumber * 5;
        monsterSpawnerController.spawnCooldown = 1f - waveNumber * 0.02f;
        monsterSpawnerController.elitesWaitingToSpawn = waveNumber;
        monsterSpawnerController.effectsPerElite = 1 + waveNumber / 5;
    }

    void NewWave()
    {
        waveNumber++;
        if(waveChangeDelegate != null)
            waveChangeDelegate();
        SpawnWave();
    }

    public void RestartGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        Time.timeScale = 1;
    }
}
