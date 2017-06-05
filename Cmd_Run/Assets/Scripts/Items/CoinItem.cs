using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CoinItem : MonoBehaviour, ICollectible {

    [Range(1, ushort.MaxValue)]
    public ushort points = 10;

    private GameController gameController;
    private PlayerStats statistics;

    public void Start()
    {
        gameController = GameObject.FindObjectOfType<GameController>();
        statistics = CmdRun.PlayerStatistics;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            statistics.AddCoin(this);
            gameController.UpdateCoinText();
            Destroy(this.gameObject);
        }
    }
}
