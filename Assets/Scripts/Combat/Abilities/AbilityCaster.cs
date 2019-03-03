using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AbilityCaster : MonoBehaviour
{

    public List<Ability> abilities;

    public delegate void OnAbilitiesChange();
    public OnAbilitiesChange onAbilitiesChange;

    //public delegate void OnAbilityCast(Ability ability);
    //public event OnAbilityCast abilityCastEvent;

    private Dictionary<Ability, AbilityContainer> abilityContainers;
    private GuiController guiController;

    private void Awake()
    {
        abilityContainers = new Dictionary<Ability, AbilityContainer>();
        foreach (Ability ability in abilities)
        {
            abilityContainers.Add(ability, new AbilityContainer(gameObject, ability));
        }
    }

    void Start()
    {
        if (onAbilitiesChange != null)
            onAbilitiesChange();
        guiController = GameObject.Find("GUI").GetComponent<GuiController>();
    }

    void Update()
    {
        foreach (AbilityContainer abilityContainer in abilityContainers.Values)
            abilityContainer.Update(Time.deltaTime);
    }


    public bool CastAbility(Ability ability, GameObject target, bool ignoreError = false)
    {
        AbilityContainer abilityContainer = abilityContainers[ability];

        if (!abilityContainer.HasMana())
        {
            if (!ignoreError)
                guiController.DisplayError("Not enough mana to use ability");
            return false;
        }
        if (!abilityContainer.IsReady())
        {
            if (!ignoreError)
                guiController.DisplayError("Ability isn't ready yet");
            return false;
        }
        return abilityContainer.UseAbility(target);
    }

    public void TriggerCooldown(Ability ability)
    {
        abilityContainers[ability].TriggerCooldown();
    }

    public void TriggerManaCost(Ability ability)
    {
        abilityContainers[ability].TriggerManaCost();
    }

    public float GetRemainingCooldown(Ability ability)
    {
        return abilityContainers[ability].GetRemainingCooldown();
    }

    public bool CanCastAbility(Ability ability)
    {
        AbilityContainer abilityContainer = abilityContainers[ability];
        return abilityContainer.IsReady() && abilityContainer.HasMana();
    }

    public void AddAbility(Ability ability)
    {
        abilities.Add(ability);
        abilityContainers.Add(ability, new AbilityContainer(gameObject, ability));
    }
}
