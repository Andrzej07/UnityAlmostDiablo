using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodicEffectContainer : LingeringEffectContainer {
    public float intervalCounter;

    public PeriodicEffectContainer(PeriodicEffect periodicEffect, GameObject source, GameObject target) : base(periodicEffect, source, target)
    {
        intervalCounter = 0;
    }

    public void Update(float deltaTime)
    {
        PeriodicEffect periodicEffect = effect as PeriodicEffect;
        intervalCounter += deltaTime;
        if(intervalCounter > periodicEffect.interval)
        {
            periodicEffect.Tick(source, target);
            intervalCounter -= periodicEffect.interval;
        }
    }
}
