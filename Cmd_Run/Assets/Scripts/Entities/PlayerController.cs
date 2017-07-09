using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerController : Controller2D {

    [SerializeField]
    private BulletController bulletPrefab = null;
    [SerializeField]
    [Range(0.01f, 100.0f)]
    private float jumpHeight = 3.0f;
    [SerializeField]
    [Range(0.01f, 100.0f)]
    private float jumpTimeToTop = 0.3f;
    [SerializeField]
    [Range(0.01f, 100.0f)]
    private float airAccelerationTime = 0.2f;
    [SerializeField]
    [Range(0.01f, 50.0f)]
    private float wallJumpVelocity = 2.0f;
    [SerializeField]
    [Range(0.01f, 50.0f)]
    private float invincibleSeconds = 2.0f;

    private IItemController gameController = null;
    private PowerUpItem currentPowerUp = null;
    private Animator animator = null;
    private BasePlatform currentPlatform;
    private Coroutine invincibleCoroutine;
    private float horizontalVelocitySmooting = 0.0f, jumpVelocity = 10.0f;

    protected override void Start () {
        base.Start();

        gameController = GameObject.FindWithTag("GameController").GetComponent<IItemController>();
        animator = this.gameObject.GetComponent<Animator>();
        RecalculateJumpPhysics();
	}

    protected override void Update () {
        base.Update();
        float inputX = WalkInputX;

        AnimatePlayer(inputX);
        CalculateMovement(inputX);

        UsePowerUp();
    }

    public void RecalculateJumpPhysics()
    {
        gravity = -(2 * jumpHeight) / Mathf.Pow(jumpTimeToTop, 2);
        jumpVelocity = Mathf.Abs(gravity) * jumpTimeToTop;
    }

    public void SetPowerUp(PowerUpItem item)
    {
        if(currentPowerUp != null)
        {
            currentPowerUp.OnPowerUpExpired -= OnPowerUpExpired;
            currentPowerUp.Cancel();
            currentPowerUp = null;
        }
        currentPowerUp = item;
        currentPowerUp.OnPowerUpExpired += OnPowerUpExpired;
        currentPowerUp.Activate();
    }

    protected virtual bool HasJumpRequested
    {
        get { return Input.GetButtonDown("Jump"); }
    }

    protected virtual float WalkInputX
    {
        get { return Input.GetAxisRaw("Horizontal"); }
    }

    private void CalculateMovement(float inputX)
    {
        bool jumpRequested = HasJumpRequested;

        if (CollisionInfo.IsCollidingAbove || CollisionInfo.IsCollidingBelow)
        {
            EnemyController enemy = null;
            if (CollisionInfo.IsCollidingBelow && CollisionInfo.VerticallyCollidingObject.TryGetComponent(out enemy))
            {
                enemy.Die(DeathCause.JumpedApon, this);
                Jump(jumpVelocity);
            }
            else if (!CollisionInfo.VerticallyCollidingObject.TryGetComponent(out currentPlatform))
            {
                currentPlatform = null;
                currentVelocity.y = 0;
            }
            else
            {
                currentVelocity.y = 0;
            }
        }
        if ((CollisionInfo.IsCollidingRight || CollisionInfo.IsCollidingLeft))
        {
            EnemyController enemy = null;
            if (CollisionInfo.HorizontallyCollidingObject.TryGetComponent(out enemy))
            {
                this.Die(DeathCause.EnemyTouched, enemy);
            }

            IPushable pushable = null;
            if (jumpRequested && !CollisionInfo.IsCollidingBelow && !CollisionInfo.IsCollidingAbove && !CollisionInfo.HorizontallyCollidingObject.CompareTag("WorldBound"))
            {
                WallJump();
            }
            else if(CollisionInfo.HorizontallyCollidingObject.TryGetComponent(out pushable))
            {
                pushable.Push(new Vector3(CurrentVelocity.x, 0));
            }
            else
            {
                currentVelocity.x = 0;
            }
        }

        if (jumpRequested && CollisionInfo.IsCollidingBelow)
        {
            Jump(jumpVelocity);
            currentPlatform = null;
        }

        float horizontalTargetVelocity = inputX * movementSpeed;
        currentVelocity.x = Mathf.SmoothDamp(currentVelocity.x, horizontalTargetVelocity, ref horizontalVelocitySmooting,
                                             (CollisionInfo.IsCollidingBelow && currentPlatform != null) ? currentPlatform.AccelerationTime : airAccelerationTime);
        currentVelocity.y += gravity * Time.deltaTime;

        Move((currentVelocity + (currentPlatform != null ? currentPlatform.CurrentVelocity : Vector3.zero)) * Time.deltaTime);
    }

    private void AnimatePlayer(float inputX)
    {
        animator.SetBool("Ground", CollisionInfo.IsCollidingBelow);
        animator.SetFloat("vSpeed", currentVelocity.y);
        animator.SetFloat("Speed", Mathf.Abs(inputX));
    }

    private void OnPowerUpExpired(object sender, EventArgs e)
    {
        currentPowerUp = null;
    }

    protected void UsePowerUp()
    {
        gameController.UsePowerUp(currentPowerUp);
        if (!Input.GetMouseButtonDown(0) || currentPowerUp == null)
        {
            return;
        }
        switch (currentPowerUp.Kind)
        {
            case PowerUpKind.Bullet:
                ShootBullet();
                break;
        }
    }
    
    public virtual void Jump(float jumpVelocity)
    {
        currentVelocity.y = jumpVelocity;
        CollisionInfo.IsCollidingBelow = false;
    }

    public override void Die(DeathCause cause, IEntity killer)
    {
        if(invincibleCoroutine == null || cause == DeathCause.Void)
        {
            GameObject.FindWithTag("GameController").GetComponent<GameController>().RespawnPlayer(true);
            currentPowerUp = null;
            invincibleCoroutine = StartCoroutine(GetInvincibleTimer());
        }
    }

    private IEnumerator GetInvincibleTimer()
    {
        yield return new WaitForSeconds(invincibleSeconds);
        invincibleCoroutine = null;
    }

    protected void WallJump()
    {
        currentVelocity.x = wallJumpVelocity * (CollisionInfo.IsCollidingRight ? -1 : 1);
        currentVelocity.y = jumpVelocity;
    }

    private void ShootBullet()
    {
        Quaternion rotation = transform.rotation;
        rotation.z = (spriteRenderer.flipX ? 180 : 0);
        BulletController c = Instantiate(bulletPrefab, transform.position, rotation);
    }
}
