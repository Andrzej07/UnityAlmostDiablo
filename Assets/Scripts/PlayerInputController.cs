using System.Collections;
using System.Collections.Generic;
using Demo.Abilities;
using Demo.Characters;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInputController : MonoBehaviour
{
    public EventSystem eventSystem;
    public string enemyTag;
    public string terrainTag;
    ICharacterMovement characterMovement;
    ICharacterCombat characterCombat;

    bool mouseActive = false;
    void Awake()
    {
        characterMovement = GetComponent<ICharacterMovement>();
        characterCombat = GetComponent<ICharacterCombat>();
        AbilitiesController abilitiesController = GetComponent<AbilitiesController>();
        abilitiesController.AbilityActivatedEvent += () =>
        {
            enabled = false;
        };
        abilitiesController.AbilityDeactivatedEvent += () =>
        {
            enabled = true;
        };
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseActive = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            mouseActive = false;
        }

        if (mouseActive && Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool raycastHit = Physics.Raycast(ray, out hit);
            if (eventSystem.IsPointerOverGameObject())
                return;
            if (raycastHit)
            {
                if (hit.transform.gameObject.tag == terrainTag)
                {
                    characterMovement.MoveTo(hit.point);
                }
                else if (hit.transform.gameObject.tag == enemyTag)
                {
                    characterCombat.Attack(hit.transform.gameObject);
                }
            }
        }
        else
        {
            int abilityInput = GetAbilityInput();
            if (abilityInput > 0)
            {
                characterCombat.ActivateAbility(abilityInput);
            }
        }
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
}
