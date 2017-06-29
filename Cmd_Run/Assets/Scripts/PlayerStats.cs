using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization;
using System.IO;

[Serializable]
public sealed class PlayerStats : ISerializable {

    public PlayerStats()
    {
        StompedEnemies = 0;
        PlayerDeaths = 0;
        PlayerPoints = 0;
        PlayerCoins = 0;
        PlayerMainCoins = 0;
    }

    public PlayerStats(SerializationInfo info, StreamingContext context)
    {
        ulong stompedEnemies = 0;
        ulong playerDeaths = 0;
        ulong playerPoints = 0;
        ulong playerCoins = 0;
        ulong playerMainCoins = 0;

        if(!info.TryGetValue("StompedEnemies", out stompedEnemies))
        {
            stompedEnemies = 0;
        }
        if(!info.TryGetValue("PlayerDeaths", out playerDeaths))
        {
            playerDeaths = 0;
        }
        if(!info.TryGetValue("PlayerPoints", out playerPoints))
        {
            playerPoints = 0;
        }
        if(!info.TryGetValue("PlayerCoins", out playerCoins))
        {
            playerCoins = 0;
        }
        if(!info.TryGetValue("PlayerMainCoins", out playerMainCoins))
        {
            playerMainCoins = 0;
        }

        StompedEnemies = stompedEnemies;
        PlayerDeaths = playerDeaths;
        PlayerPoints = playerPoints;
        PlayerCoins = playerCoins;
        PlayerMainCoins = playerMainCoins;
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("StompedEnemies", StompedEnemies);
        info.AddValue("PlayerDeaths", PlayerDeaths);
        info.AddValue("PlayerPoints", PlayerPoints);
        info.AddValue("PlayerCoins", PlayerCoins);
        info.AddValue("PlayerMainCoins", PlayerMainCoins);
    }

    public ulong StompedEnemies { get; private set; }
    public ulong PlayerDeaths { get; private set; }
    public ulong PlayerPoints { get; private set; }

    public ulong PlayerCoins { get; private set; }
    public ulong PlayerMainCoins { get; private set; }

    public void AddCoin(CoinItem item)
    {
        PlayerCoins++;
        AddPlayerPoints(item.points);
    }
    public void AddMainCoin(CoinMainItem item)
    {
        PlayerMainCoins++;
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
