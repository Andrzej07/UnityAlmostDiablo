using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LingeringEffectContainer {

    public LingeringEffect effect;
    public float remainingDuration;
    public GameObject source;
    public GameObject target;

    public LingeringEffectContainer(LingeringEffect lingeringEffect, GameObject source, GameObject target)
    {
        this.source = source;
        this.target = target;
        effect = lingeringEffect;
        remainingDuration = effect.duration;
    }

    public void UpdateDuration(float deltaTime)
    {
        remainingDuration -= deltaTime;
    }

    public bool IsFinished()
    {
        return remainingDuration <= 0;
    }

    public void Reapply()
    {
        remainingDuration = effect.duration;
    }

    public void OnRemove()
    {
        effect.OnRemove(source, target);
    }
}
