using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public Effect[] eliteEffects;
    public int effectsPerElite = 1;
    public GameObject eliteDropPrefab;

    private GameController gameController;
    private Transform monsterParent;
    private float timeSinceSpawn = 0;
    private Transform pickupParent;

    // Use this for initialization
    void Start()
    {
        gameController = GameController.instance;
        monsterParent = GameObject.Find("Enemies").transform;
        pickupParent = GameObject.Find("Pickups").transform;
    }

    // Update is called once per frame
    void Update()
    {
        int currentEnemyCount = monsterParent.childCount;
        if (currentEnemyCount >= maxMonstersInScene)
            return;

        timeSinceSpawn += Time.deltaTime;
        if (timeSinceSpawn > spawnCooldown && waitingToSpawn + elitesWaitingToSpawn > 0)
        {
            GameObject newMonster = Instantiate(monsterPrefab, monsterParent);
            int spawnIdx = Mathf.RoundToInt(Random.value * (spawnPositions.Length - 1));
            newMonster.transform.position = spawnPositions[spawnIdx].position;
            newMonster.GetComponent<MonsterController>().player = player;
            timeSinceSpawn = 0;
            if (eliteEffects.Length > 0 && Random.Range(0, elitesWaitingToSpawn + waitingToSpawn) < elitesWaitingToSpawn)
            {
                Debug.Log("Spawning elite");
                MakeElite(newMonster);
                elitesWaitingToSpawn--;
            }
            else
            {
                waitingToSpawn--;
            }

            if (gameController.enemySpawnedDelegate != null)
                gameController.enemySpawnedDelegate();
        }
    }

    void MakeElite(GameObject newMonster)
    {
        List<int> available = Enumerable.Range(0, eliteEffects.Length).ToList();
        int effectCounter = effectsPerElite;
        while (effectCounter-- > 0 && available.Count > 0)
        {
            int rand = Random.Range(0, available.Count);
            eliteEffects[rand].ApplyEffect(newMonster, newMonster);
            available.Remove(rand);
        }
        newMonster.GetComponentInChildren<SkinnedMeshRenderer>().material = eliteMaterial;
        newMonster.GetComponent<DefenseController>().deathDelegate += SpawnPickup;
    }

    void SpawnPickup(GameObject monster)
    {
        GameObject drop = Instantiate(eliteDropPrefab, pickupParent);
        drop.transform.position = monster.transform.position;
    }


}
