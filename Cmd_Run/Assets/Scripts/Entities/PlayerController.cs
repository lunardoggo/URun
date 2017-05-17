﻿using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller2D {

    [Range(0.01f, 100.0f)]
    public float jumpHeight = 3.0f;
    [Range(0.01f, 100.0f)]
    public float jumpTimeToTop = 0.3f;
    [Range(0.01f, 100.0f)]
    public float groundAccelerationTime = 0.1f;
    [Range(0.01f, 100.0f)]
    public float airAccelerationTime = 0.2f;
    [Range(0.01f, 50.0f)]
    public float wallJumpVelocity = 2.0f;

    private float jumpVelocity = 10.0f;
    private float horizontalVelocitySmooting = 0.0f;

    protected override void Start () {
        base.Start();

        RecalculateJumpPhysics();
	}

    protected override void Update () {
        base.Update();
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), 0);

        if(CollisionInfo.IsCollidingAbove || CollisionInfo.IsCollidingBelow)
        {
            EnemyController enemy = CollisionInfo.VerticallyCollidingObject.GetComponentIfNotNull<EnemyController>();
            if (CollisionInfo.IsCollidingBelow && enemy != null)
            {
                enemy.Die(DeathCause.JumpedApon, this);
                Jump();
            }
            else
            {
                currentVelocity.y = 0;
            }
        }
        if (CollisionInfo.IsCollidingRight || CollisionInfo.IsCollidingLeft)
        {
            EnemyController enemy = CollisionInfo.HorizontallyCollidingObject.GetComponentIfNotNull<EnemyController>();
            if(enemy != null)
            {
                this.Die(DeathCause.EnemyTouched, enemy);
            }

            if (!CollisionInfo.IsCollidingBelow && !CollisionInfo.IsCollidingAbove && Input.GetKeyDown(KeyCode.Space))
            {
                WallJump();
            }
            else
            {
                currentVelocity.x = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && CollisionInfo.IsCollidingBelow)
        {
            Jump();
        }

        float horizontalTargetVelocity = input.x * moveSpeed;
        currentVelocity.x = Mathf.SmoothDamp(currentVelocity.x, horizontalTargetVelocity, ref horizontalVelocitySmooting, 
                                             (CollisionInfo.IsCollidingBelow) ? groundAccelerationTime : airAccelerationTime);
        currentVelocity.y += gravity * Time.deltaTime;
        Move(currentVelocity * Time.deltaTime);
	}

    public void RecalculateJumpPhysics()
    {
        gravity = -(2 * jumpHeight) / Mathf.Pow(jumpTimeToTop, 2);
        jumpVelocity = Mathf.Abs(gravity) * jumpTimeToTop;
    }

    private void Jump()
    {
        currentVelocity.y = jumpVelocity;
        CollisionInfo.IsCollidingBelow = false;
    }

    private void WallJump()
    {
        currentVelocity.x = wallJumpVelocity * (CollisionInfo.IsCollidingRight ? -1 : 1);
        currentVelocity.y = jumpVelocity;
    }

    public override void Die(DeathCause cause, IEntity killer)
    {
        GameObject.FindWithTag("GameController").GetComponent<GameController>().RespawnPlayer(true);
    }
}