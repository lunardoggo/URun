using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPadController : Controller2D, IEntity {

    [SerializeField]
    [Range(0.01f, 50.0f)]
    private float jumpTimeToTop = 0.3f;
    [SerializeField]
    [Range(0.01f, 50.0f)]
    private float jumpHeight = 4;

    protected override void Start()
    {
        base.Start();
        applyGravity = false;
    }

    protected override void Update () {
        UpdateRaycasts();
        CollisionInfo.Reset();

        PlayerController player = null;
        Vector3 up = Vector3.up * 0.1f;
        if(GetFirstVerticalCollision(ref up, 1).TryGetComponent(out player))
        {
            player.Jump(GetVelocity());
        }
	}

    /// <summary>
    /// Berechnet die Sprunkraft für den oberhalb kollidierenden <see cref="Controller2D"/>;
    /// </summary>
    private float GetVelocity()
    {
        return Mathf.Abs((2 * jumpHeight) / Mathf.Pow(jumpTimeToTop, 2)) * jumpTimeToTop;
    }

    /// <summary>
    /// Zerstört das übergeordnete <see cref="GameObject"/>
    /// </summary>
    public override void Die(DeathCause cause, IEntity killer)
    {
        Destroy(this.gameObject);
    }
}
