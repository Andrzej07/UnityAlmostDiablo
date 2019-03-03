using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : ScriptableObject {
    public abstract void ApplyEffect(GameObject source, GameObject target);
}
