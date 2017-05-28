using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovingPlatformController : BasePlatform {

    [SerializeField]
    private PlatformMovementMode movementMode = PlatformMovementMode.Once;
    [SerializeField]
    private bool startOnPlayerCollision = true;
    [SerializeField]
    [Range(-0.999999f, 0.999999f)]
    private float followProgress = 0.0f;

    private BezierSpline path;
    private bool move = false, forward = true;
    private float moveSpeed = 0.0f;

    protected override void Start () {
        base.Start();
        move = !startOnPlayerCollision;
        moveSpeed = Mathf.Pow(movementSpeed, -1);
        path = GetComponent<BezierSpline>();
	}

    protected override void Update () {
        base.Update();
        Move(Vector3.zero);
	}

    /// <summary>
    /// Bewegt diesen <see cref="MovingPlatformController"/> auf seiner <see cref="BezierSpline"/> unabhängig von der velocity vorwärts
    /// </summary>
    protected override void Move(Vector3 velocity)
    {
        UpdateRaycasts();
        CollisionInfo.Reset();
        Vector3 up = Vector3.up * RayLength;

        if (move)
        {
            if (forward)
            {
                followProgress += Time.deltaTime * moveSpeed;
                if (followProgress > 1.0f)
                {
                    PathEnd();
                }
            }
            else
            {
                followProgress -= Time.deltaTime * moveSpeed;
                if (followProgress < 0.0f)
                {
                    followProgress *= -1;
                    forward = true;
                }
            }

            Vector3 position = transform.position;
            transform.position = path.GetPoint(followProgress) + originPosition;
            deltaPosition = transform.position - position;
        }
        PlayerController player = null;
        if(GetFirstVerticalCollision(ref up, 1).TryGetComponent(out player))
        {
            if(startOnPlayerCollision && !move)
            {
                move = true;
            }
            player.transform.Translate(deltaPosition);
        }
    }

    /// <summary>
    /// Wenn das Ende der <see cref="BezierSpline"/> erreicht wurde, wird eine Aktion abhängig von <see cref="movementMode"/> ausgelöst
    /// </summary>
    private void PathEnd()
    {
        switch(movementMode)
        {
            case PlatformMovementMode.Once:
                move = false;
                applyGravity = false;
                currentVelocity = Vector3.zero;
                break;
            case PlatformMovementMode.Loop:
                followProgress -= 1.0f;
                break;
            case PlatformMovementMode.PingPong:
                followProgress = 2.0f - followProgress;
                forward = false;
                break;
        }
    }
}

public enum PlatformMovementMode : byte
{
    Once = 0,
    Loop = 1,
    PingPong = 2,
}