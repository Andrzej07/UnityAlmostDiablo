using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Demo.Characters;
using Demo.Combat;
using UnityEngine;

public class MonsterSpawnerController : MonoBehaviour
{

    public Transform player;
    public GameObject monsterPrefab;
    public Transform[] spawnPositions;
    public int maxMonstersInScene = 100;
    public float spawnCooldown = 1;
    public int waitingToSpawn = 5;
    public Material eliteMaterial;
    public int elitesWaitingToSpawn = 0;
    public EliteMonsterEffect[] eliteEffects;
    public int effectsPerElite = 1;
    public GameObject eliteDropPrefab;

    public event Action<GameObject> MonsterSpawnedEvent;

    private Transform monsterParent;
    private float timeSinceSpawn = 0;
    private Transform pickupParent;

    void Start()
    {
        monsterParent = GameObject.Find("Enemies").transform;
        pickupParent = GameObject.Find("Pickups").transform;
    }

    void Update()
    {
        int currentEnemyCount = monsterParent.childCount;
        if (currentEnemyCount >= maxMonstersInScene)
            return;

        timeSinceSpawn += Time.deltaTime;
        if (timeSinceSpawn > spawnCooldown && waitingToSpawn + elitesWaitingToSpawn > 0)
        {
            GameObject newMonster = Instantiate(monsterPrefab, monsterParent);
            int spawnIdx = Mathf.RoundToInt(UnityEngine.Random.value * (spawnPositions.Length - 1));
            newMonster.transform.position = spawnPositions[spawnIdx].position;
            newMonster.GetComponent<MonsterController>().player = player;
            timeSinceSpawn = 0;
            if (eliteEffects.Length > 0 && UnityEngine.Random.Range(0, elitesWaitingToSpawn + waitingToSpawn) < elitesWaitingToSpawn)
            {
                Debug.Log("Spawning elite");
                MakeElite(newMonster);
                elitesWaitingToSpawn--;
            }
            else
            {
                waitingToSpawn--;
            }

            if (MonsterSpawnedEvent != null)
                MonsterSpawnedEvent(newMonster);
        }
    }

    void MakeElite(GameObject newMonster)
    {
        List<int> available = Enumerable.Range(0, eliteEffects.Length).ToList();
        int effectCounter = effectsPerElite;
        while (effectCounter-- > 0 && available.Count > 0)
        {
            int rand = UnityEngine.Random.Range(0, available.Count);
            eliteEffects[rand].ApplyEffect(newMonster, newMonster);
            available.Remove(rand);
        }
        newMonster.GetComponentInChildren<SkinnedMeshRenderer>().material = eliteMaterial;
        newMonster.GetComponent<IDamageable>().DeathEvent += SpawnPickup;
    }

    void SpawnPickup(GameObject monster)
    {
        GameObject drop = Instantiate(eliteDropPrefab, pickupParent);
        drop.transform.position = monster.transform.position;
    }


}
