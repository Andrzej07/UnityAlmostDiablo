using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LingeringEffect : Effect {
    public float duration;
    
    override public void ApplyEffect(GameObject source, GameObject target)
    {
        LingeringEffectsController controller = target.GetComponent<LingeringEffectsController>();
        if (controller != null)
            controller.AddEffect(this, source);
    }

    public abstract void OnRemove(GameObject source, GameObject target);
}
