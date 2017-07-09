using System;
using System.Collections.Generic;
using UnityEngine;

public interface IItemController : ILevelController
{
    void AddHealth();
    void CollectedMainCoin(CoinMainItem item);
    void AddCoin(CoinItem item);
    void UsePowerUp(PowerUpItem item);
    bool HasCollectedMainCoin { get; }
}

public interface ILevelController
{
    event EventHandler<CheckpointEventArgs> OnCheckpointChanged;
    PlayerController Player { get; }
    bool PlayerIsAlive { get; }
    Checkpoint CurrentCheckpoint { get; set; }
    void RespawnPlayer(bool restartLevel);
}

public interface IMusicController
{
    void PlayNextTrack();
}