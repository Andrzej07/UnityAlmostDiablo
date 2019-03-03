using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    public delegate void OnProjectileTriggerEnterDelegate(Collider other);
    public OnProjectileTriggerEnterDelegate onProjectileTriggerEnter;

    public delegate void OnProjectileEndOfLifeDelegate();
    public OnProjectileEndOfLifeDelegate onProjectileEndOfLife;

    [HideInInspector]
    public float timeToLive;
    [HideInInspector]
    public float velocity;
    [HideInInspector]
    public Vector3 direction;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += velocity * direction * Time.deltaTime;
        timeToLive -= Time.deltaTime;
        if(timeToLive < 0)
        {
            if (onProjectileEndOfLife != null)
                onProjectileEndOfLife();
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (onProjectileTriggerEnter != null)
            onProjectileTriggerEnter(other);
    }

    private void OnDestroy()
    {
        onProjectileTriggerEnter = null;
        onProjectileEndOfLife = null;
    }
}
