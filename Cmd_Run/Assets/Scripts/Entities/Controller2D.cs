using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D), typeof(SpriteRenderer))]
public abstract class Controller2D : MonoBehaviour, IEntity {

    [SerializeField]
    [Range(0.01f, 50.0f)]
    protected float moveSpeed = 4;
    [SerializeField]
    [Range(2, byte.MaxValue)]
    protected int verticalRayCount = 5;
    [SerializeField]
    [Range(2, byte.MaxValue)]
    protected int horizontalRayCount = 5;
    [SerializeField]
    protected LayerMask collisionLayers;

    public IEntity Killer { get; protected set; }
    public bool IsAlive { get; protected set; }

    protected CollisionInfo2D CollisionInfo;
    protected Vector3 currentVelocity = Vector3.zero;
    protected float gravity = -9.81f;
    protected SpriteRenderer spriteRenderer;

    private const float colliderInset = 0.015f;
    private float horizontalRaySpacing;
    private float verticalRaySpacing;
    private RaycastOrigins2D raycastOrigins;
    private BoxCollider2D boxCollider;

    protected virtual void Start () {
        boxCollider = GetComponent<BoxCollider2D>();

        Killer = null;
        IsAlive = true;

        spriteRenderer = this.gameObject.GetComponentIfNotNull<SpriteRenderer>();
        GetRaySpacing();
    }

    protected virtual void Update () { }

    protected void UpdateRaycasts()
    {
        Bounds bounds = boxCollider.bounds;
        bounds.Expand(colliderInset * -2);

        raycastOrigins.BottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.BottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.TopLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.TopRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    private void GetRaySpacing()
    {
        Bounds bounds = boxCollider.bounds;
        bounds.Expand(colliderInset * -2);

        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
    }

    protected virtual void Move(Vector3 velocity)
    {
        UpdateRaycasts();
        CollisionInfo.Reset();

        if (velocity.x != 0)
        {
            spriteRenderer.flipX = (velocity.x < 0);
            CollisionInfo.HorizontallyCollidingObject = GetHorizontalCollisions(ref velocity, Mathf.Sign(velocity.x));
        }
        if(velocity.y != 0)
        {
            CollisionInfo.VerticallyCollidingObject = GetVerticalCollisions(ref velocity, Mathf.Sign(velocity.y));
        }
        transform.Translate(velocity);
    }

    protected GameObject GetVerticalCollisions(ref Vector3 velocity, float verticalDirection)
    {
        List<GameObject> collidingObjects = new List<GameObject>();
        float rayLength = Mathf.Abs(velocity.y) + colliderInset;

        for(int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (verticalDirection == -1 ? raycastOrigins.BottomLeft : raycastOrigins.TopLeft) + Vector2.right * (verticalRaySpacing * i + velocity.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * verticalDirection, rayLength, collisionLayers);

            Debug.DrawRay(rayOrigin, Vector2.up * verticalDirection * rayLength, Color.green);

            if (hit)
            {
                velocity.y = (hit.distance - colliderInset) * verticalDirection;
                rayLength = hit.distance;
                CollisionInfo.IsCollidingAbove = (verticalDirection == 1);
                CollisionInfo.IsCollidingBelow = (verticalDirection == -1);

                return hit.collider.gameObject;
            }
        }

        return null;
    }

    protected GameObject GetHorizontalCollisions(ref Vector3 velocity, float horizontalDirection)
    {
        float rayLength = Mathf.Abs(velocity.x) + colliderInset;

        for(int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (horizontalDirection == -1 ? raycastOrigins.BottomLeft : raycastOrigins.BottomRight) + Vector2.up * horizontalRaySpacing * i;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * horizontalDirection, rayLength, collisionLayers);

            Debug.DrawRay(rayOrigin, Vector2.right * horizontalDirection * rayLength, Color.green);

            if(hit)
            {
                velocity.x = (hit.distance - colliderInset) * horizontalDirection;
                rayLength = hit.distance;
                CollisionInfo.IsCollidingRight = (horizontalDirection == 1);
                CollisionInfo.IsCollidingLeft = (horizontalDirection == -1);

                return hit.collider.gameObject;
            }
        }
        return null;
    }

    public abstract void Die(DeathCause cause, IEntity killer);
}

public struct RaycastOrigins2D
{
    public Vector2 TopLeft { get; set; }
    public Vector2 TopRight { get; set; }
    public Vector2 BottomLeft { get; set; }
    public Vector2 BottomRight { get; set; }
}

public struct CollisionInfo2D
{
    public bool IsCollidingAbove { get; set; }
    public bool IsCollidingBelow { get; set; }
    public bool IsCollidingLeft { get; set; }
    public bool IsCollidingRight { get; set; }

    public GameObject HorizontallyCollidingObject { get; set; }
    public GameObject VerticallyCollidingObject { get; set; }

    public void Reset()
    {
        IsCollidingAbove = IsCollidingBelow = IsCollidingLeft = IsCollidingRight = false;
        HorizontallyCollidingObject = null;
        VerticallyCollidingObject = null;
    }
}