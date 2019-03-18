using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using Demo.Abilities;
using Demo.Characters;
using Demo.Combat;

public class PlayerController : CharacterController
{
    private GuiController guiController;

    new void Awake()
    {
        base.Awake();
        IDamageable damageable = GetComponent<IDamageable>();
        damageable.DeathEvent += OnDeath;
    }

    void Start()
    {
        guiController = GameController.instance.guiController;
    }

    /* private void LateUpdate()
    {
        if (agent.velocity.magnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(agent.velocity);
        }
    } */

    private void OnDeath(GameObject gameObject)
    {
        Debug.Log("GAME OVER");
        guiController.ShowGameOverPopup();
        Time.timeScale = 0;
    }
}
