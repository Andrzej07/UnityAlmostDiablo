using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{

    private NavMeshAgent agent;
    private Animator animator;
    private AbilityCaster abilityCaster;
    private DefenseController defense;
    private IOrder currentOrder;
    private GuiController guiController;
    private EventSystem eventSystem;
    private GameObject fakeTarget;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        abilityCaster = GetComponent<AbilityCaster>();
        defense = GetComponent<DefenseController>();
        defense.deathDelegate += OnDeath;
    }

    // Use this for initialization
    void Start()
    {
        agent.updateRotation = false;
        guiController = GameObject.Find("GUI").GetComponent<GuiController>();
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        fakeTarget = GameObject.Find("AbilityTarget");
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool raycastHit = Physics.Raycast(ray, out hit);

        if (Input.GetMouseButton(0))
        {
            if (eventSystem.IsPointerOverGameObject())
                return;
            if (raycastHit)
            {
                if (hit.transform.gameObject.tag == "Terrain" && !agent.pathPending)
                {
                    ChangeOrder(new MoveOrder(agent, hit.point));
                }
                else if (hit.transform.gameObject.tag == "Enemy")
                {
                    ChangeOrder(new CastAbilityOrder(abilityCaster, abilityCaster.abilities[0], hit.transform.gameObject, true));
                }
            }
        }

        GameObject target = null;
        if (raycastHit)
        {
            if (hit.transform.gameObject.tag == "Terrain")
            {
                target = fakeTarget;
                fakeTarget.transform.position = hit.point;
            }
            else if (hit.transform.gameObject.tag == "Enemy")
            {
                target = hit.transform.gameObject;
            }
        }
        int abilityInput = GetAbilityInput();
        if (abilityInput >= 0)
            ChangeOrder(new CastAbilityOrder(abilityCaster, abilityCaster.abilities[abilityInput], target));
        if (currentOrder != null && !currentOrder.Finished)
        {
            currentOrder.Perform();
        }

        animator.SetBool("Run", agent.velocity.magnitude > 0);
    }

    private void LateUpdate()
    {
        if (agent.velocity.magnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(agent.velocity);
        }
    }

    private void OnDeath(GameObject gameObject)
    {
        Debug.Log("GAME OVER");
        guiController.ShowGameOverPopup();
        Time.timeScale = 0;
    }

    int GetAbilityInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            return 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            return 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            return 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            return 4;
        }
        return -1;
    }

    bool ChangeOrder(IOrder newOrder)
    {
        if (currentOrder == null || currentOrder.Finished || currentOrder.Interruptible)
        {
            currentOrder = newOrder;
            return true;
        }
        return false;
    }
}
