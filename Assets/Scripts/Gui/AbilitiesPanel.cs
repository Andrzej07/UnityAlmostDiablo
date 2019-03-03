using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitiesPanel : MonoBehaviour {

    public AbilityCaster playerAbilities;
    public Text[] abilityLabels;
    public Text[] cooldownLabels;

    private void Awake()
    {
        playerAbilities.onAbilitiesChange += UpdateAbilityLabels;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        UpdateCooldowns();
	}

    void UpdateAbilityLabels()
    {
        for(int i = 0; i < abilityLabels.Length; i++)
        {
            Tooltip tooltip = abilityLabels[i].gameObject.GetComponent<Tooltip>();            
            if (i < playerAbilities.abilities.Count)
            {
                abilityLabels[i].text = playerAbilities.abilities[i].abilityName;
                if (tooltip != null)
                {
                    tooltip.text = playerAbilities.abilities[i].tooltip;
                }
            } else
            {
                abilityLabels[i].text = "No ability assigned";
                if(tooltip != null)
                {
                    tooltip.text = null;
                }
            }
        }
    }

    void UpdateCooldowns()
    {
        for (int i = 0; i < abilityLabels.Length; i++)
        {
            if (i < playerAbilities.abilities.Count)
            {
                cooldownLabels[i].enabled = true;
                cooldownLabels[i].text =  playerAbilities.GetRemainingCooldown(playerAbilities.abilities[i]).ToString("0.#");
            }
            else
            {
                cooldownLabels[i].enabled = false;
            }
        }
            
    }
}
