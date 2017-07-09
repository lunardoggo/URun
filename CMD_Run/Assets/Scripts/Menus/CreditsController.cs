using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsController : MonoBehaviour, IItemController {

    [SerializeField]
    private float secondsToWait = 5.0f;

    private Coroutine endTimerRoutine;
    private Checkpoint currentCheckpoint;

    public event EventHandler<CheckpointEventArgs> OnCheckpointChanged;
    public bool HasCollectedMainCoin { get { return true; } }
    public PlayerController Player { get; private set; }
    public bool PlayerIsAlive { get { return Player != null && Player.IsAlive; } }
    public Checkpoint CurrentCheckpoint
    {
        get { return currentCheckpoint; }
        set
        {
            if (value != null && currentCheckpoint != value)
            {
                currentCheckpoint = value;
                if (OnCheckpointChanged != null)
                    OnCheckpointChanged.Invoke(this, new CheckpointEventArgs(value));
            }
        }
    }

    private void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnDestroy()
    {
        this.StopCoroutine(ref endTimerRoutine);
    }

    private void Update () {
		if(Input.GetButtonDown("Fire1"))
        {
            GameTools.LoadMainMenu();
        }
	}


    public void AddHealth() {}

    public void CollectedMainCoin(CoinMainItem item) {}

    public void AddCoin(CoinItem item) {}

    public void UsePowerUp(PowerUpItem item) { }

    public void RespawnPlayer(bool restartLevel)
    {
        if (restartLevel)
            Player.transform.position = currentCheckpoint.transform.position;
        else
            GameTools.LoadMainMenu();
    }
}
