using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{

    public Transform player;
    public int experienceReward = 10;
    public string monsterName = "Monster";

    GameController gameController;
    DefenseController defense;
    AbilityCaster abilityCaster;
    Animator animator;
    NavMeshAgent agent;
    NavMeshObstacle obstacle;

    DefenseController playerDefense;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        obstacle = GetComponent<NavMeshObstacle>();
        animator = GetComponent<Animator>();
        defense = GetComponent<DefenseController>();
        abilityCaster = GetComponent<AbilityCaster>();
        defense.deathDelegate += OnDeath;
    }

    // Use this for initialization
    void Start()
    {
        gameController = GameObject.Find("Game").GetComponent<GameController>();
        playerDefense = player.GetComponent<DefenseController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!defense.isDead)
        {
            animator.SetFloat("Speed", agent.velocity.magnitude);
            if (!playerDefense.isDead)
            {
                // assume all abilities are offensive & priorities are determined by their position in the list
                for (int i = abilityCaster.abilities.Count - 1; i >= 0; i--)
                {
                    Ability ability = abilityCaster.abilities[i];
                    if (abilityCaster.CanCastAbility(ability))
                    {
                        abilityCaster.CastAbility(ability, player.gameObject, true);
                    }
                }
                if (abilityCaster.abilities[0].IsInPosition(gameObject, player.gameObject))
                {
                    agent.enabled = false;
                    obstacle.enabled = true;
                    obstacle.carving = true;
                }
                else
                {
                    obstacle.carving = false;
                    StartCoroutine(ReenableAgent());
                }
            }
        }
    }

    IEnumerator ReenableAgent()
    {
        yield return null;
        obstacle.enabled = false;
        agent.enabled = true;
    }

    private void OnMouseOver()
    {
        GetComponentInChildren<Renderer>().material.SetFloat("_Outline", 0.1f);
    }

    private void OnMouseExit()
    {
        GetComponentInChildren<Renderer>().material.SetFloat("_Outline", 0);
    }

    private void OnDeath(GameObject dyingObject)
    {
        if (gameController.enemyDeathDelegate != null)
            gameController.enemyDeathDelegate(gameObject);
        GetComponent<Collider>().enabled = false;
        agent.enabled = false;
        obstacle.enabled = false;
        agent.velocity = Vector3.zero;
        StartCoroutine(DestroyAfterDelay(5));
    }

    private IEnumerator DestroyAfterDelay(int delay)
    {
        yield return new WaitForSeconds(delay);
        ObjectDestructionController.Destroy2(gameObject);
    }

}
