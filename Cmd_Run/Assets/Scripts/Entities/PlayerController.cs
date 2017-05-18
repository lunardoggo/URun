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
    private float groundAccelerationTime = 0.1f;
    [SerializeField]
    [Range(0.01f, 100.0f)]
    private float airAccelerationTime = 0.2f;
    [SerializeField]
    [Range(0.01f, 50.0f)]
    private float wallJumpVelocity = 2.0f;

    private GameController gameController = null;
    private PowerUpItem currentPowerUp = null;
    private float jumpVelocity = 10.0f;
    private float horizontalVelocitySmooting = 0.0f;
    private Animator animator = null;

    protected override void Start () {
        base.Start();

        gameController = FindObjectOfType<GameController>();
        animator = this.gameObject.GetComponentIfNotNull<Animator>();
        RecalculateJumpPhysics();
	}

    protected override void Update () {
        base.Update();
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), 0);

        if(CollisionInfo.IsCollidingAbove || CollisionInfo.IsCollidingBelow)
        {
            EnemyController enemy = CollisionInfo.VerticallyCollidingObject.GetComponentIfNotNull<EnemyController>();
            if (CollisionInfo.IsCollidingBelow && enemy != null)
            {
                enemy.Die(DeathCause.JumpedApon, this);
                Jump();
            }
            else
            {
                currentVelocity.y = 0;
            }
        }
        if (CollisionInfo.IsCollidingRight || CollisionInfo.IsCollidingLeft)
        {
            EnemyController enemy = CollisionInfo.HorizontallyCollidingObject.GetComponentIfNotNull<EnemyController>();
            if(enemy != null)
            {
                this.Die(DeathCause.EnemyTouched, enemy);
            }

            if (!CollisionInfo.IsCollidingBelow && !CollisionInfo.IsCollidingAbove && Input.GetKeyDown(KeyCode.Space))
            {
                WallJump();
            }
            else
            {
                currentVelocity.x = 0;
            }
        }

        animator.SetBool("Ground", CollisionInfo.IsCollidingBelow);
        animator.SetFloat("vSpeed", currentVelocity.y);
        animator.SetFloat("Speed", Mathf.Abs(currentVelocity.x));

        if (Input.GetKeyDown(KeyCode.Space) && CollisionInfo.IsCollidingBelow)
        {
            Jump();
        }

        float horizontalTargetVelocity = input.x * moveSpeed;
        currentVelocity.x = Mathf.SmoothDamp(currentVelocity.x, horizontalTargetVelocity, ref horizontalVelocitySmooting, 
                                             (CollisionInfo.IsCollidingBelow) ? groundAccelerationTime : airAccelerationTime);
        currentVelocity.y += gravity * Time.deltaTime;
        Move(currentVelocity * Time.deltaTime);

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

    private void OnPowerUpExpired(object sender, EventArgs e)
    {
        Debug.Log("Expired");
        currentPowerUp = null;
    }

    private void UsePowerUp()
    {
        gameController.UpdatePowerUpText(currentPowerUp == null ? (ushort)0 : currentPowerUp.Duration);
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

    private void ShootBullet()
    {
        Quaternion rotation = transform.rotation;
        rotation.z = (spriteRenderer.flipX ? 180 : 0);
        BulletController c = Instantiate(bulletPrefab, transform.position, rotation);
    }

    private void Jump()
    {
        currentVelocity.y = jumpVelocity;
        CollisionInfo.IsCollidingBelow = false;
    }

    private void WallJump()
    {
        currentVelocity.x = wallJumpVelocity * (CollisionInfo.IsCollidingRight ? -1 : 1);
        currentVelocity.y = jumpVelocity;
    }

    public override void Die(DeathCause cause, IEntity killer)
    {
        GameObject.FindWithTag("GameController").GetComponent<GameController>().RespawnPlayer(true);
        currentPowerUp = null;
    }
}
