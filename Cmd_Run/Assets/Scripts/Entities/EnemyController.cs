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
        currentVelocity.x = -movementSpeed; 
    }

    protected override void Update()
    {
        base.Update();

        if (CollisionInfo.IsCollidingBelow)
        {
            currentVelocity.y = 0;
            PlayerController player = null;
            if(CollisionInfo.VerticallyCollidingObject.TryGetComponent(out player))
            {
                player.Die(DeathCause.JumpedApon, this);
                currentVelocity.y = stompPlayerJumpHeight;
            }
        }

        if (CollisionInfo.IsCollidingRight || CollisionInfo.IsCollidingLeft)
        {
            currentVelocity.x *= -1;
            PlayerController player = null;
            if (CollisionInfo.HorizontallyCollidingObject.TryGetComponent(out player))
            {
                player.Die(DeathCause.EnemyTouched, this);
            }
        }

        currentVelocity.x = Mathf.SmoothDamp(currentVelocity.x, movementSpeed * Mathf.Sign(currentVelocity.x), ref horizontalVelocitySmooting, groundAccelerationTime);
        currentVelocity.y += gravity * Time.deltaTime;
        Move(currentVelocity * Time.deltaTime);
    }

    /// <summary>
    /// Zerstört das übergeordnete <see cref="GameObject"/> und aktualisiert die Spielerstatistik
    /// </summary>
    public override void Die(DeathCause cause, IEntity killer)
    {
        if(cause == DeathCause.JumpedApon && killer is PlayerController)
        {
            PlayerStats.Instance.AddStompedEnemy();
        }
        Destroy(this.gameObject);
    }
}
