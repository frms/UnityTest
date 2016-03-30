﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Inventory : MonoBehaviour
{
    public GameObject slotSample;
    public GameObject line;
    public Transform unit;

    Pool slotPool;

    public List<Item> items;

    public Item selected;

    public event Action onChanged = () => { };

    void Awake() {
        slotPool = new Pool(slotSample);
    }

    public void Pick(Item item) {
        items.Insert(0, item);
        if (selected == null) {
            selected = item;
        }

        Debug.Log(string.Format("Pick {0}", item));
        GameObject slotObject = slotPool.Take();
        InventorySlot slot = slotObject.GetComponent<InventorySlot>();
        slot.Init(this, item);

        onChanged();
    }

    public void Throw(Item item) {
        if (selected == item) {
            selected = items.CyclicNext(selected);
        }
        items.Remove(item);
        Debug.Log(string.Format("Throw {0}", item));

        item.transform.SetParent(null, worldPositionStays: false);
        item.transform.position = unit.position;

        onChanged();
    }

    void Update() {
        if (Input.GetButtonDown("Next Item")) {
            selected = items.CyclicNext(selected);
            onChanged();
        }
        if (Input.GetButtonDown("Previous Item")) {
            selected = items.CyclicNext(selected, -1);
            onChanged();
        }
        if (Input.GetButtonDown("Throw") && selected != null) {
            Throw(selected);
            onChanged();
        }
    }
}