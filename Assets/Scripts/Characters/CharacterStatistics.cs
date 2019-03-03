using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatistics : MonoBehaviour {

    public delegate void OnStatsChangeDelegate();
    public OnStatsChangeDelegate statsChangeDelegate;

    public int maxHealth;
    public int health;
    public int armor;
    public float maxMana;
    public float mana;
    public float manaRegenerationRate;
    public int strength;
    public int intelligence;

    private void Awake()
    {
        health = maxHealth;
        mana = maxMana;
    }

    void Start () {
		
	}
	
	void Update () {
        mana += manaRegenerationRate * Time.deltaTime;
        if (mana > maxMana)
            mana = maxMana;

        if (statsChangeDelegate != null)
            statsChangeDelegate();
    }
}
