using System.Collections;
using System.Collections.Generic;
using Demo.Characters;
using UnityEngine;

public class HealthbarController : MonoBehaviour
{

    private Transform remainingHealth;

    private Health health;

    void Start()
    {
        ICombatStatistics stats = transform.GetComponentInParent<ICombatStatistics>();
        health = stats.Health;
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.gameObject.name == "RemainingHealth")
            {
                remainingHealth = child;
            }
        }
    }

    void Update()
    {
        float remainingPercent = health.CurrentHealth / (float)health.MaxHealth;
        remainingHealth.localScale = new Vector3(remainingPercent, 1, 1);
        transform.rotation = Camera.main.transform.rotation;
    }
}
