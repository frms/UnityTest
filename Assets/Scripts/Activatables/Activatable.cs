﻿using UnityEngine;
using UnityEngine.Events;

public abstract class Activatable : MonoBehaviour
{
    public float maxDistance = 0;
    public bool useActivatorMaxDistance = true;

    public virtual void Activate() {
        Debug.Log(string.Format("Activated: {0}", this));
    }

    public float EffectiveMaxDistance() {
        if (useActivatorMaxDistance) {
            return Activator.instance.maxDistance;
        }
        return maxDistance;
    }
}