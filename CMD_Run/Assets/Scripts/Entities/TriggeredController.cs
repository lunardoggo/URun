using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggeredController : PlayerController {

    private float walkSpeed = 1.0f;
    private bool hasJumpRequested = false;

    protected override bool HasJumpRequested
    {
        get { return (hasJumpRequested); }
    }

    protected override float WalkInputX
    {
        get { return walkSpeed; }
    }

    public override void Jump(float jumpVelocity)
    {
        base.Jump(jumpVelocity);
        hasJumpRequested = false;
    }

    public override void Die(DeathCause cause, IEntity killer)
    {
        if (cause == DeathCause.Void)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerActionController actionController = null;
        if(collision.gameObject.TryGetComponent(out actionController))
        {
            Debug.Log("Action requested: " + actionController.Action);
            switch(actionController.Action)
            {
                case TriggerAction.Walk:
                    walkSpeed = 1.0f;
                    break;
                case TriggerAction.Stop:
                    walkSpeed = 0.0f;
                    break;
                case TriggerAction.Jump:
                    hasJumpRequested = true;
                    break;
                case TriggerAction.UsePowerUp:
                    UsePowerUp();
                    break;
                case TriggerAction.DirectionChange:
                    walkSpeed *= -1;
                    break;
            }
        }
    }
}
