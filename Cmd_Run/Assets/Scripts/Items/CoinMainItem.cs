using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CoinMainItem : MonoBehaviour, ICollectible
{

    [Range(1, ushort.MaxValue)]
    public ushort points = 10;

    private IItemController controller;

    public void Start()
    {
        controller = GameObject.FindWithTag("GameController").GetComponent<IItemController>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            controller.CollectedMainCoin(this);
            Destroy(this.gameObject);
        }
    }
}
