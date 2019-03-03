using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarController : MonoBehaviour {

    private Transform remainingHealth;
    //private Transform lostHealth;

    private CharacterStatistics stats;

	// Use this for initialization
	void Start () {
        stats = transform.parent.GetComponent<CharacterStatistics>();
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if(child.gameObject.name == "RemainingHealth")
            {
                remainingHealth = child;
            } /*else if(child.gameObject.name == "LostHealth")
            {
                lostHealth = child;
            } */
        }
        
	}
	
	// Update is called once per frame
	void Update () {
        float remainingPercent = stats.health / (float) stats.maxHealth;
        //lostHealth.localScale = new Vector3(1.0f - remainingPercent, 1, 1);
        remainingHealth.localScale = new Vector3(remainingPercent, 1, 1);
        //transform.LookAt(Camera.main.transform);
        transform.rotation = Camera.main.transform.rotation;
    }
}
