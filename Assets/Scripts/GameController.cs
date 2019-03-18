using System;
using System.Collections;
using System.Collections.Generic;
using Demo.Combat;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public static GameController instance;
    public GuiController guiController;

    public event Action waveChangeEvent;

    public event Action<int> EnemyCountChangedEvent;

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
        monsterSpawnerController.MonsterSpawnedEvent += OnMonsterSpawnedEvent;
    }

    // Use this for initialization
    void Start()
    {
        SpawnWave();
        enemies = GameObject.Find("Enemies").transform;
        enemyCount = enemies.childCount;
        if (EnemyCountChangedEvent != null)
            EnemyCountChangedEvent(enemyCount);
    }

    // Update is called once per frame
    void Update()
    {

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
        if (waveChangeEvent != null)
            waveChangeEvent();
        SpawnWave();
    }

    public void RestartGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        Time.timeScale = 1;
    }

    void OnMonsterSpawnedEvent(GameObject newMonster)
    {
        enemyCount++;
        if (EnemyCountChangedEvent != null)
            EnemyCountChangedEvent(enemyCount);

        newMonster.GetComponent<IDamageable>().DeathEvent += OnMonsterDeathEvent;
    }

    void OnMonsterDeathEvent(GameObject deadMonster)
    {
        enemyCount--;
        if (enemyCount == 0 && monsterSpawnerController.waitingToSpawn == 0)
        {
            NewWave();
        }
        if (EnemyCountChangedEvent != null)
            EnemyCountChangedEvent(enemyCount);
    }
}
