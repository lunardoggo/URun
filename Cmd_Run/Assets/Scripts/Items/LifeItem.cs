﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class LifeItem : MonoBehaviour, CollectableItem {

    private GameController gameController;
    private PlayerStats statistics;

	public void Start () {
        gameController = GameObject.FindObjectOfType<GameController>();
        statistics = PlayerStats.Instance;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            //TODO: Leben hinzufügen
            Destroy(this.gameObject);
        }
    }
}