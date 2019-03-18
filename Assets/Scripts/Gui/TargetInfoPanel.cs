using System.Collections;
using System.Collections.Generic;
using Demo.Characters;
using UnityEngine;
using UnityEngine.UI;

public class TargetInfoPanel : MonoBehaviour
{
    public CombatStatsPanel combatStatsPanel;

    public Text targetName;
    private Image image;

    // Use this for initialization
    void Start()
    {
        //combatStatsPanel = GetComponent<CombatStatsPanel>();
        image = GetComponent<Image>();
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.tag == "Enemy")
            {
                combatStatsPanel.target = hit.transform.gameObject;
                UpdateName(hit.transform.gameObject.GetComponent<MonsterController>().monsterName);
                Show();
            }
            else
            {
                Hide();
            }
        }
        else
        {
            Hide();
        }
    }

    void UpdateName(string name)
    {
        targetName.text = name;
    }

    void Show()
    {
        //gameObject.SetActive(true);
        image.enabled = true;
        targetName.enabled = true;
        combatStatsPanel.enabled = true;
    }

    void Hide()
    {
        combatStatsPanel.enabled = false;
        targetName.enabled = false;
        image.enabled = false;
        //gameObject.SetActive(false);
    }
}
