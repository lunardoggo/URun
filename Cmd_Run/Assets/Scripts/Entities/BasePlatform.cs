using System;
using System.Collections.Generic;
using UnityEngine;

public class BasePlatform : Controller2D, IEntity {

    public event EventHandler<EventArgs> OnPlatformDestroyed;

    /// <summary>
    /// Positionsunterschied zwischen diesem und dem letzten Update
    /// </summary>
    protected Vector3 deltaPosition = Vector3.zero;
    /// <summary>
    /// Startposition der Platform
    /// </summary>
    protected Vector3 originPosition = Vector3.zero;

    protected const float RayLength = 0.1f;

    protected override void Awake () {
        applyGravity = false;
        originPosition = transform.position;
	}

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (OnPlatformDestroyed != null)
        {
            OnPlatformDestroyed.Invoke(this, new EventArgs());
        }
    }

    public override void Die(DeathCause cause, IEntity killer)
    {
        Destroy(this.gameObject);
    }
}
