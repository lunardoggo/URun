using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class TriggerActionController : MonoBehaviour {

    [SerializeField]
    private TriggerAction action;

    public TriggerAction Action
    {
        get { return action; }
    }
}

public enum TriggerAction : byte
{
    Walk = 0,
    Stop = 1,
    Jump = 2,
    UsePowerUp = 3,
    DirectionChange = 4,
}