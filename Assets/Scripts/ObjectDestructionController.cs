using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestructionController : MonoBehaviour
{
    public bool isScheduledForDestruction = false;
    private List<LingeringEffectContainer> effects;

    public delegate void OnSafeToDestroy(GameObject gameObject);
    public event OnSafeToDestroy onSafeToDestroy;

    // Use this for initialization
    void Awake()
    {
        effects = new List<LingeringEffectContainer>();
    }

    // Update is called once per frame
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
                effectContainer.target.GetComponent<LingeringEffectsController>().lingeringEffectExpire += OnEffectExpire;
                effectContainer.target.GetComponent<DefenseController>().deathDelegate += OnTargetDeath;
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
        if (IsSafeToDestroy() && onSafeToDestroy != null)
        {
            onSafeToDestroy(this.gameObject);
        }
    }

    public static void Destroy2(GameObject gameObject)
    {        
        ObjectDestructionController odc = gameObject.GetComponent<ObjectDestructionController>();
        if (odc == null || odc.IsSafeToDestroy())
        {
            Destroy(gameObject);
        }
        else
        {
            odc.isScheduledForDestruction = true;
            odc.onSafeToDestroy += Destroy;
            for(int i = 0; i < gameObject.transform.childCount; i++) 
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
}
