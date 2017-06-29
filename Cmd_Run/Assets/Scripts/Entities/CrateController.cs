using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateController : Controller2D, IPushable
{
    [SerializeField, Range(0.05f, 100.0f)]
    private float mass = 15.0f;

    protected override void Start()
    {
        base.Start();
        BoxCollider.size = spriteRenderer.size;
    }

    protected override void Update()
    {
        base.Update();

        if (!CollisionInfo.IsCollidingBelow)
        {
            currentVelocity.y += gravity * Time.deltaTime * mass;
        }
        else
        {
            currentVelocity.y = 0;
            IEntity entity = null;
            if(CollisionInfo.VerticallyCollidingObject.TryGetComponent(out entity))
            {
                entity.Die(DeathCause.JumpedApon, this);
            }
        }

        Move(currentVelocity * Time.deltaTime);
    }

    public void Push(Vector3 velocity)
    {
        Move(velocity * Time.deltaTime * (10.0f / mass));
    }

    public override void Die(DeathCause cause, IEntity killer)
    {
        if (cause == DeathCause.Void)
        {
            Destroy(this.gameObject);
        }
    }
}
