using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : BasePlatform {

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
        if (applyGravity)
        {
            Vector3 lastPosition = transform.position;
            currentVelocity.y += gravity * Time.deltaTime;
            Move(currentVelocity * Time.deltaTime);
            deltaPosition = transform.position - lastPosition;
        }
        else
        {
            if (!onlyFallOnAboveCollision)
            {
                FallOnCollision();
            }
        }
	}

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if(fallLaterRoutine != null)
        {
            StopCoroutine(fallLaterRoutine);
            fallLaterRoutine = null;
        }
        
    }

    private void FallOnAboveCollision()
    {
        Vector3 up = Vector3.up * RayLength;
        GameObject collidingObject = null;
        if((collidingObject = GetFirstVerticalCollision(ref up, 1)) != null)
        {
            fallLaterRoutine = StartCoroutine(FallLater());
            PlayerController player = null;
            if(collidingObject.TryGetComponent(out player))
            {
                player.transform.Translate(deltaPosition);
            }
        }
    }

    private IEnumerator FallLater()
    {
        yield return new WaitForSeconds(fallDelay);
        applyGravity = true;
    }

    private void FallOnCollision()
    {
        Vector3 left = Vector3.left * RayLength;
        Vector3 right = Vector3.right * RayLength;
        Vector3 down = Vector3.down * RayLength;

        if(GetFirstHorizontalCollision(ref right, 1) != null || GetFirstHorizontalCollision(ref left, -1) != null || GetFirstVerticalCollision(ref down, -1) != null)
        {
            applyGravity = true;
        }
    }
}
