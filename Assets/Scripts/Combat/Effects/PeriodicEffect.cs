using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PeriodicEffect : LingeringEffect
{
    public float interval;

    /* override public void ApplyEffect(GameObject source, GameObject target)
    {
        LingeringEffectsController controller = target.GetComponent<LingeringEffectsController>();
        if(controller != null)
            controller.AddEffect(this, source);
    } */

    public abstract void Tick(GameObject source, GameObject target);
}
