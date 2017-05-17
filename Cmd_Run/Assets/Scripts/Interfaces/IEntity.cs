﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntity  {

    void Die(DeathCause cause, IEntity killer);
    IEntity Killer { get; }
    bool IsAlive { get; }
}

public enum DeathCause : byte
{
    Trigger = 0,
    JumpedApon = 1,
    EnemyTouched = 2
}