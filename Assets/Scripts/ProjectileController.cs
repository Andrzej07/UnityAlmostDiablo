using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{

    public delegate void OnProjectileTriggerEnterDelegate(Collider other);
    public event OnProjectileTriggerEnterDelegate onProjectileTriggerEnter;

    public delegate void OnProjectileEndOfLifeDelegate();
    public event OnProjectileEndOfLifeDelegate onProjectileEndOfLife;

    [HideInInspector]
    public float timeToLive;
    [HideInInspector]
    public float velocity;
    [HideInInspector]
    public Vector3 direction;
    [HideInInspector]
    public GameObject source;
    public bool destroyOnHit;

    public Effect[] effects;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position += velocity * direction * Time.deltaTime;
        timeToLive -= Time.deltaTime;
        if (timeToLive < 0)
        {
            if (onProjectileEndOfLife != null)
                onProjectileEndOfLife();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Projectile hit: " + other.gameObject.name);
        if (GameController.instance.AreHostile(source, other.gameObject))
        {
            foreach (Effect effect in effects)
                effect.ApplyEffect(source, other.gameObject);
            if (destroyOnHit)
                Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Terrain")
        {
            Destroy(gameObject);
        }


        if (onProjectileTriggerEnter != null)
            onProjectileTriggerEnter(other);
    }

    private void OnDestroy()
    {
        onProjectileTriggerEnter = null;
        onProjectileEndOfLife = null;
    }
}
