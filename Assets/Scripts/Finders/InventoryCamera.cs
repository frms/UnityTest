﻿using UnityEngine;
using System.Collections;

public class InventoryCamera : MonoBehaviour
{
    float animationDelay = 0.25f;

    public static GameObject instance;

    public Inventory inventory;

    TransformAnimator transformAnimator;

    void Awake() {
        transformAnimator = GetComponent<TransformAnimator>();
        instance = gameObject;
        inventory.onChanged += OnInventoryChanged;
    }

    int offset() {
        int index = inventory.items.IndexOf(inventory.selected);
        int count = inventory.items.Count;
        if (count <= 7) {
            return 0;
        }
        return Mathf.Clamp(index - 3, 0, count - 7);
    }

    Vector3 TargetPosition() {
        return new Vector3(2 * offset(), 0, 0);
    }

    Vector3 TargetScale() {
        return Vector3.one;
    }

    void OnInventoryChanged() {
        transformAnimator.Animate(new TimedValue<TransformState>(new TransformState(TargetPosition(), TargetScale()), Time.time + animationDelay));
    }
}