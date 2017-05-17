using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PowerUpItem : MonoBehaviour, CollectableItem {

    private GameController gameController;
    private PlayerStats statistics;

    public void Start()
    {
        gameController = GameObject.FindObjectOfType<GameController>();
        statistics = PlayerStats.Instance;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //TODO: PowerUp ausführen
            Destroy(this.gameObject);
        }
    }
}
