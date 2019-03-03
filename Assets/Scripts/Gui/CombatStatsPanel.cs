using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatStatsPanel : MonoBehaviour {
    public GameObject labelPrefab;
    public GameObject target;

    private CharacterStatistics stats;
    private Text health;
    private Text mana;
    private Text armor;
    private Text strength;
    private Text intelligence;
    private List<Text> labels;

    private void Awake()
    {
        labels = new List<Text>();
        health = Instantiate(labelPrefab, gameObject.transform).GetComponent<Text>();
        mana = Instantiate(labelPrefab, gameObject.transform).GetComponent<Text>();
        armor = Instantiate(labelPrefab, gameObject.transform).GetComponent<Text>();
        strength = Instantiate(labelPrefab, gameObject.transform).GetComponent<Text>();
        intelligence = Instantiate(labelPrefab, gameObject.transform).GetComponent<Text>();
        labels.Add(health);
        labels.Add(armor);
        labels.Add(strength);
        labels.Add(intelligence);
        labels.Add(mana);
        OnDisable();
    }

    void OnEnable () {
        if (target != null)
        {
            stats = target.GetComponent<CharacterStatistics>();
            UpdateStats();
            stats.statsChangeDelegate += UpdateStats;
            foreach (Text label in labels)
            {
                label.enabled = true;
            }
        }
    }	

    private void UpdateStats()
    {
        strength.text = string.Format("Strength: {0}", stats.strength);
        intelligence.text = string.Format("Intelligence: {0}", stats.intelligence);
        health.text = string.Format("Health: {0} / {1}", stats.health, stats.maxHealth);
        mana.text = string.Format("Mana: {0} / {1}", Mathf.RoundToInt(stats.mana), Mathf.RoundToInt(stats.maxMana));
        armor.text = string.Format("Armor: {0}", stats.armor);       
    }

    private void OnDisable()
    {
        if (stats != null)
        {
            stats.statsChangeDelegate -= UpdateStats;
        }
        foreach (Text label in labels)
        {
            label.enabled = false;
        }
    }
}
