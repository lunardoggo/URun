using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovingPlatformController : Controller2D, IEntity {

    public PlatformMovementMode movementMode = PlatformMovementMode.FallOff;
    public bool startOnPlayerCollision = true;
    public List<Vector3> movementPath;

    private Vector3 nextWaypoint = Vector3.zero;
    private bool move = false;
    private bool applyGravity = false;

    protected override void Start () {
        base.Start();
        move = !startOnPlayerCollision;
        if(movementPath.Count > 0)
        {
            nextWaypoint = movementPath[0];
        }

        var count = this.verticalRayCount;
	}

    protected override void Update () {
        Vector3 vector = Vector3.zero;
        

        if(move)
        {
            Move(Vector3.MoveTowards(transform.position, nextWaypoint, moveSpeed));
            if (transform.position == nextWaypoint)
            {
                int index = movementPath.IndexOf(nextWaypoint);
                if (movementPath.Count > index)
                {
                    nextWaypoint = movementPath[index + 1];
                }
                else
                {
                    PathEnd();
                }
            }
        }
        else
        {
            GameObject player = GetVerticalCollisions(ref vector, 1);
            Debug.Log(player);
            if (player != null)
            {
                move = true;
            }

            if (applyGravity)
            {
                currentVelocity.y += -9.81f * Time.deltaTime;
            }
            Move(currentVelocity * Time.deltaTime);
        }

        Debug.Log(move);
	}

    protected override void Move(Vector3 velocity)
    {
        UpdateRaycasts();
        transform.Translate(velocity);
    }

    private void PathEnd()
    {
        switch(movementMode)
        {
            case PlatformMovementMode.FallOff:
                move = false;
                applyGravity = true;
                break;
            case PlatformMovementMode.Stop:
                currentVelocity = Vector3.zero;
                nextWaypoint = movementPath[0];
                movementPath.Reverse();
                break;
            case PlatformMovementMode.TeleportToStart:
                transform.position = movementPath[0];
                nextWaypoint = movementPath[0];
                break;
            case PlatformMovementMode.TravelBack:
                movementPath.Reverse();
                break;
        }
    }

    public override void Die(DeathCause cause, IEntity killer)
    {
        Destroy(this);
    }
}

public enum PlatformMovementMode : byte
{
    FallOff = 0,
    Stop = 1,
    TravelBack = 2,
    TeleportToStart = 3
}