using System;
using System.Collections;
using System.Collections.Generic;
using Demo.Combat;
using UnityEngine;

public class ObjectDestructionController : MonoBehaviour
{
    public bool isScheduledForDestruction = false;
    private List<LingeringEffectContainer> effects;
    public event Action<GameObject> SafeToDestroyEvent;

    IDamageable damageable;
    LingeringEffectsController effectsController;

    void Awake()
    {
        effects = new List<LingeringEffectContainer>();
    }

    void Update()
    {

    }

    public bool IsSafeToDestroy()
    {
        return effects.Count == 0;
    }

    public void AddEffect(LingeringEffectContainer effectContainer)
    {
        if (effectContainer.target != gameObject)
        {
            if (effects.Find(item => item == effectContainer) == null)
            {
                effectsController = effectContainer.target.GetComponent<LingeringEffectsController>();
                if (effectsController != null)
                {
                    effectsController.EffectExpireEvent += OnEffectExpire;
                }
                damageable = effectContainer.target.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.DeathEvent += OnTargetDeath;
                }
            }
            effects.Add(effectContainer);
        }
    }

    private void OnEffectExpire(LingeringEffectContainer effectContainer)
    {
        effects.RemoveAll(item => item == effectContainer);
        Notify();
    }

    private void OnTargetDeath(GameObject gameObject)
    {
        effects.RemoveAll(item => item.target == gameObject);
        Notify();
    }

    private void Notify()
    {
        if (IsSafeToDestroy() && SafeToDestroyEvent != null)
        {
            SafeToDestroyEvent(this.gameObject);
        }
    }

    public static void Destroy2(GameObject gameObject)
    {
        ObjectDestructionController odc = gameObject.GetComponent<ObjectDestructionController>();
        if (odc == null || odc.IsSafeToDestroy())
        {
            Destroy(gameObject);
        }
        else if (!odc.isScheduledForDestruction)
        {
            odc.isScheduledForDestruction = true;
            odc.SafeToDestroyEvent += Destroy;
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }
            foreach (Behaviour component in gameObject.GetComponents<Behaviour>())
            {
                Debug.Log("Disabling " + component.name);
                component.enabled = false;
            }
            odc.enabled = true;
        }
    }

    void OnDestroy()
    {
        if (damageable != null)
        {
            damageable.DeathEvent -= OnTargetDeath;
        }
        if (effectsController != null)
        {
            effectsController.EffectExpireEvent -= OnEffectExpire;
        }
    }
}
