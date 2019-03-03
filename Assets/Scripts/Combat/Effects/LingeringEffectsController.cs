using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LingeringEffectsController : MonoBehaviour
{

    [SerializeField]
    private List<LingeringEffectContainer> lingeringEffects;

    private DefenseController defenseController;

    public delegate void OnLingeringEffectExpire(LingeringEffectContainer lingeringEffectContainer);
    public OnLingeringEffectExpire lingeringEffectExpire;

    private void Awake()
    {
        lingeringEffects = new List<LingeringEffectContainer>();
        //periodicEffects = new List<PeriodicEffectContainer>();
        defenseController = GetComponent<DefenseController>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (defenseController.isDead)
            return;
        foreach (LingeringEffectContainer le in lingeringEffects.ToArray())
        {
            le.UpdateDuration(Time.deltaTime);
            if (le.IsFinished())
            {
                lingeringEffects.Remove(le);
                le.OnRemove();
                if (lingeringEffectExpire != null)
                    lingeringEffectExpire(le);
            }
            else if (le is PeriodicEffectContainer)
            {
                ((PeriodicEffectContainer)le).Update(Time.deltaTime);
            }
        }
        /* foreach (PeriodicEffectContainer pe in periodicEffects.ToArray()) {
            pe.UpdateDuration(Time.deltaTime);
            if(pe.IsFinished())
            {
                periodicEffects.Remove(pe);
                pe.OnRemove();
            } 
            else 
            {
                pe.Update(Time.deltaTime);
            }
        }*/
    }

    public void AddEffect(LingeringEffect lingeringEffect, GameObject source)
    {
        Debug.Log("Adding lingering effect " + lingeringEffect.GetType() + " to: " + gameObject.name);
        LingeringEffectContainer existingEffect = lingeringEffects.Find(x => x.effect == lingeringEffect && x.source == source);
        if (existingEffect != null)
        {
            existingEffect.Reapply();
        }
        else
        {
            LingeringEffectContainer lingeringEffectContainer;
            if (lingeringEffect is PeriodicEffect)
                lingeringEffectContainer = new PeriodicEffectContainer((PeriodicEffect)lingeringEffect, source, gameObject);
            else
                lingeringEffectContainer = new LingeringEffectContainer(lingeringEffect, source, gameObject);
            lingeringEffects.Add(lingeringEffectContainer);
            ObjectDestructionController odc = source.GetComponent<ObjectDestructionController>();
            if (odc == null)
            {
                odc = source.AddComponent<ObjectDestructionController>();
            }
            odc.AddEffect(lingeringEffectContainer);
        }
    }

    /* public void AddEffect(PeriodicEffect periodicEffect, GameObject source)
    {
        Debug.Log("Adding periodic effect " + periodicEffect.GetType() + " to: " + gameObject.name);
        PeriodicEffectContainer existingEffect = periodicEffects.Find(x => x.effect == periodicEffect && x.source == source);
        if (existingEffect != null)
        {
            existingEffect.Reapply();
        } 
        else 
        {
            periodicEffects.Add(new PeriodicEffectContainer(periodicEffect, source, gameObject));
            MarkSource(source);
        }
    }*/

}
