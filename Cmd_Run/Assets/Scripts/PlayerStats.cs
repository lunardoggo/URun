using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerStats {

    #region Singleton-Pattern

    private static PlayerStats instance = null;

    public static PlayerStats Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new PlayerStats();
            }
            return instance;
        }
    }

    #endregion

    private PlayerStats()
    {
        //StompedEnemies, PlayerDeaths und PlayerPoints aus bspw. Config auslesen

        PlayerCoins = 0;
    }

    public ulong StompedEnemies { get; private set; }
    public ulong PlayerDeaths { get; private set; }
    public ulong PlayerPoints { get; private set; }

    public ulong PlayerCoins { get; private set; }

    public void AddCoin(CoinItem item)
    {
        PlayerCoins++;
        AddPlayerPoints(item.points);
    }

    public void AddPlayerPoints(ushort points)
    {
        PlayerPoints += points;
    }

    public void AddPlayerDeath()
    {
        PlayerDeaths++;
    }

    public void AddStompedEnemy()
    {
        StompedEnemies++;
    }
}
