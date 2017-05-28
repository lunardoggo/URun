using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CoinMainItem : MonoBehaviour, ICollectible
{

    [Range(1, ushort.MaxValue)]
    public ushort points = 10;

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
            statistics.AddMainCoin(this);
            gameController.UpdateMainCoinText();
            Destroy(this.gameObject);
        }
    }
}
