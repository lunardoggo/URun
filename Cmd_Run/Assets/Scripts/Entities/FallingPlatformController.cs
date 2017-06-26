using System.Collections;
using UnityEngine;
using System;

public class FallingPlatformController : BasePlatform {

    public event EventHandler<EventArgs> OnPlatfromFalling;

    [SerializeField]
    private bool onlyFallOnAboveCollision = true;
    [SerializeField]
    [Range(0.01f, 100.0f)]
    private float fallDelay = 3.0f;

    private Coroutine fallLaterRoutine = null;

    protected override void Start () {
        base.Start();
	}

    protected override void Update () {
        base.Update();
        UpdateRaycasts();
        CollisionInfo.Reset();

        FallOnAboveCollision();
        if (!onlyFallOnAboveCollision)
        {
            FallOnCollision();
        }
        if (applyGravity)
        {
            Vector3 lastPosition = transform.position;
            currentVelocity.y += gravity * Time.deltaTime;
            Controller2D below = null;
            Move(currentVelocity * Time.deltaTime);
            if (CollisionInfo.IsCollidingBelow && CollisionInfo.VerticallyCollidingObject.TryGetComponent(out below))
            {
                below.Die(DeathCause.JumpedApon, this);
            }
            deltaPosition = transform.position - lastPosition;
        }
	}

    /// <summary>
    /// Beim Zerstören wird die <see cref="fallLaterRoutine"/> angehalten und aus dem Speicher entfernt, falls sie noch existiert
    /// </summary>
    protected override void OnDestroy()
    {
        base.OnDestroy();
        if(fallLaterRoutine != null)
        {
            StopCoroutine(fallLaterRoutine);
            fallLaterRoutine = null;
        }
    }

    /// <summary>
    /// Wenn ein <see cref="PlayerController"/> mit dieser <see cref="FallingPlatformController"/> auf der Oberseite kollidiert, wird die <see cref="fallLaterRoutine"/>-<see cref="Coroutine"/> gestartet
    /// </summary>
    private void FallOnAboveCollision()
    {
        Vector3 up = Vector3.up * RayLength;
        PlayerController player = null;
        if (GetFirstVerticalCollision(ref up, 1).TryGetComponent(out player))
        {
            player.transform.Translate(deltaPosition);
            if(fallLaterRoutine == null)
            {
                fallLaterRoutine = StartCoroutine(FallLater());
            }
        }
    }

    /// <summary>
    /// Sorgt dafür, dass diese <see cref="FallingPlatformController"/> nach Ablauf des Timers fällt
    /// </summary>
    private IEnumerator FallLater()
    {
        Debug.Log("fall");
        yield return new WaitForSeconds(fallDelay);
        applyGravity = true;
        if(OnPlatfromFalling != null)
        {
            OnPlatfromFalling.Invoke(this, new EventArgs());
        }
    }

    /// <summary>
    /// Wein ein <see cref="PlayerController"/> 
    /// </summary>
    private void FallOnCollision()
    {
        Vector3 left = Vector3.left * RayLength;
        Vector3 right = Vector3.right * RayLength;
        Vector3 down = Vector3.down * RayLength;

        PlayerController player = null;
        //Nicht oben, da dies schon in Update aufgerufen wird
        if(   GetFirstHorizontalCollision(ref right, 1).TryGetComponent(out player) 
           || GetFirstHorizontalCollision(ref left, -1).TryGetComponent(out player) 
           || GetFirstVerticalCollision(ref down, -1).TryGetComponent(out player))
        {
            player.transform.Translate(deltaPosition);
            if (fallLaterRoutine == null)
            {
                fallLaterRoutine = StartCoroutine(FallLater());
            }
        }
    }
}
