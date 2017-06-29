using System;
using System.Collections.Generic;
using UnityEngine;

public class BasePlatform : Controller2D, IEntity {

    public event EventHandler<EventArgs> OnPlatformDestroyed;

    /// <summary>
    /// Gibt an, wieviel Zeit ein Controller2D benötigt, um auf volle Geschwindigkeit zu beschleunigen
    /// </summary>
    public float AccelerationTime { get { return accelerationTime; } }

    /// <summary>
    /// Positionsunterschied zwischen diesem und dem letzten Update
    /// </summary>
    protected Vector3 deltaPosition = Vector3.zero;
    /// <summary>
    /// Startposition der Platform
    /// </summary>
    protected Vector3 originPosition = Vector3.zero;

    protected const float RayLength = 0.1f;

    [SerializeField]
    [Range(0.01f, 25.0f)]
    private float accelerationTime = 0.1f;

    protected override void Start()
    {
        base.Start();
        BoxCollider.size = spriteRenderer.size;
    }

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
        if (cause == DeathCause.JumpedApon || cause == DeathCause.Void)
            return;
        Destroy(this.gameObject);
    }
}
