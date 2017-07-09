using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class LifeItem : MonoBehaviour, ICollectible {

    private IItemController controller;

	public void Start () {
        controller = GameObject.FindWithTag("GameController").GetComponent<IItemController>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            controller.AddHealth();
            Destroy(this.gameObject);
        }
    }
}
