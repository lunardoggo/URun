using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyController : Controller2D {

    [SerializeField]
    [Range(0.01f, 100.0f)]
    private float groundAccelerationTime = 0.1f;
    private float stompPlayerJumpHeight = 2.0f;
    private float horizontalVelocitySmooting;

    protected override void Start()
    {
        base.Start();
        currentVelocity.x = -moveSpeed; 
    }

    protected override void Update()
    {
        base.Update();

        if (CollisionInfo.IsCollidingBelow)
        {
            currentVelocity.y = 0;
            PlayerController player = CollisionInfo.VerticallyCollidingObject.GetComponentIfNotNull<PlayerController>();
            if(player != null)
            {
                player.Die(DeathCause.JumpedApon, this);
                currentVelocity.y = stompPlayerJumpHeight;
            }
        }

        if (CollisionInfo.IsCollidingRight || CollisionInfo.IsCollidingLeft)
        {
            currentVelocity.x *= -1;
            PlayerController player = CollisionInfo.HorizontallyCollidingObject.GetComponentIfNotNull<PlayerController>();
            if (player != null)
            {
                player.Die(DeathCause.EnemyTouched, this);
            }
        }

        currentVelocity.x = Mathf.SmoothDamp(currentVelocity.x, moveSpeed * Mathf.Sign(currentVelocity.x), ref horizontalVelocitySmooting, groundAccelerationTime);
        currentVelocity.y += gravity * Time.deltaTime;
        Move(currentVelocity * Time.deltaTime);
    }

    public override void Die(DeathCause cause, IEntity killer)
    {
        if(cause == DeathCause.JumpedApon && killer is PlayerController)
        {
            PlayerStats.Instance.AddStompedEnemy();
        }
        Destroy(this.gameObject);
    }
}
