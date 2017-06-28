using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D), typeof(SpriteRenderer))]
public abstract class Controller2D : MonoBehaviour, IEntity {

    public event EventHandler<CollisionInfoEventArgs> OnCollision;

    [SerializeField]
    [Range(0.000001f, 50.0f)]
    protected float movementSpeed = 4;
    [SerializeField]
    [Range(2, byte.MaxValue)]
    protected int verticalRayCount = 5;
    [SerializeField]
    [Range(2, byte.MaxValue)]
    protected int horizontalRayCount = 5;
    [SerializeField]
    protected LayerMask collisionLayers;
    [SerializeField]
    protected bool applyGravity = true;

    public IEntity Killer { get; protected set; }
    public bool IsAlive { get; protected set; }
    public Vector3 CurrentVelocity { get { return currentVelocity; } }
    public float Gravity { get { return gravity; } }

    protected CollisionInfo2D CollisionInfo;
    protected Vector3 currentVelocity = Vector3.zero;
    protected float gravity = -9.81f;
    protected SpriteRenderer spriteRenderer;

    protected BoxCollider2D BoxCollider { get; private set; }

    private const float colliderInset = 0.015f;
    private float horizontalRaySpacing;
    private float verticalRaySpacing;
    private RaycastOrigins2D raycastOrigins;

    protected virtual void Start () {
        BoxCollider = GetComponent<BoxCollider2D>();

        Killer = null;
        IsAlive = true;

        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        GetRaySpacing();
    }

    protected virtual void Awake() { }
    protected virtual void Update () { }
    protected virtual void OnDestroy() { }

    protected void UpdateRaycasts()
    {
        if(BoxCollider == null)
        {
            BoxCollider = GetComponent<BoxCollider2D>();
        }

        Bounds bounds = BoxCollider.bounds;
        bounds.Expand(colliderInset * -2);

        raycastOrigins.BottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.BottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.TopLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.TopRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    private void GetRaySpacing()
    {
        Bounds bounds = BoxCollider.bounds;
        bounds.Expand(colliderInset * -2);

        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
    }

    /// <summary>
    /// Errechnet die Bewegung dieses <see cref="Controller2D"/>s anhand der übergebenen velocity
    /// </summary>
    protected virtual void Move(Vector3 velocity)
    {
        UpdateRaycasts();
        CollisionInfo.Reset();

        if (velocity.x != 0)
        {
            spriteRenderer.flipX = (velocity.x < 0);
            CollisionInfo.HorizontallyCollidingObject = GetFirstHorizontalCollision(ref velocity, velocity.x);
        }
        if(velocity.y != 0)
        {
            CollisionInfo.VerticallyCollidingObject = GetFirstVerticalCollision(ref velocity, velocity.y);
        }
        transform.Translate(velocity);
    }

    protected GameObject GetFirstVerticalCollision(ref Vector3 velocity, float verticalDirection)
    {
        float rayLength = Mathf.Abs(velocity.y) + colliderInset;
        verticalDirection = Mathf.Sign(verticalDirection);

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (verticalDirection == -1 ? raycastOrigins.BottomLeft : raycastOrigins.TopLeft) + Vector2.right * (verticalRaySpacing * i + velocity.x);
            RaycastHit2D hit = CastRayVertically(rayOrigin, verticalDirection, collisionLayers, rayLength, ref velocity);
            if (hit)
            {
                if (OnCollision != null)
                    OnCollision.Invoke(this, new CollisionInfoEventArgs(hit.collider.gameObject));

                return hit.collider.gameObject;
            }
        }
        return null;
    }

    /// <summary>
    /// Gibt das erste <see cref="GameObject"/>, das ein <see cref="Ray"/> in der Horizontalen trifft zurück
    /// </summary>
    /// <param name="velocity">Ausgangsbewegungskraft (Länge der Strahlen hängt davon ab)</param>
    /// <param name="horizontalDirection">Richtung der <see cref="Ray"/>s (-1 = links, sonst rechts)</param>
    protected GameObject GetFirstHorizontalCollision(ref Vector3 velocity, float horizontalDirection)
    {
        float rayLength = Mathf.Abs(velocity.x) + colliderInset;
        horizontalDirection = Mathf.Sign(horizontalDirection);

        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (horizontalDirection == -1 ? raycastOrigins.BottomLeft : raycastOrigins.BottomRight) + Vector2.up * horizontalRaySpacing * i;
            RaycastHit2D hit = CastRayHorizontally(rayOrigin, horizontalDirection, collisionLayers, rayLength, ref velocity);
            if(hit)
            {
                if (OnCollision != null)
                    OnCollision.Invoke(this, new CollisionInfoEventArgs(hit.collider.gameObject));

                return hit.collider.gameObject;
            }
        }
        return null;
    }

    /// <summary>
    /// Wirft einen <see cref="Ray"/> und gibt dessen <see cref="RaycastHit2D"/> zurück
    /// </summary>
    /// <param name="origin">Ursprung des <see cref="Ray"/>s</param>
    /// <param name="horizontalDirection">Horizontale Richtung des ausgesendeten <see cref="Ray"/>s (-1 = links, 1 = rechts)</param>
    /// <param name="collisionLayers"><see cref="LayerMask"/>s, mit denen der <see cref="Ray"/> kollidieren kann</param>
    /// <param name="rayLength">Länge des ausgesendeten <see cref="Ray"/>s</param>
    /// <param name="velocity">Bewegungsrichtung, die in der Funktion abgeändert wird, wenn der ausgesendete <see cref="Ray"/> auf ein Objekt trifft</param>
    private RaycastHit2D CastRayHorizontally(Vector2 origin, float horizontalDirection, LayerMask collisionLayers, float rayLength, ref Vector3 velocity)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.right * horizontalDirection, rayLength, collisionLayers);
        Debug.DrawRay(origin, Vector2.right * horizontalDirection * rayLength, Color.green);

        if(hit)
        {
            velocity.x = (hit.distance - colliderInset) * horizontalDirection;
            rayLength = hit.distance; //TODO: benötigt?
            CollisionInfo.IsCollidingRight = (horizontalDirection == 1);
            CollisionInfo.IsCollidingLeft = (horizontalDirection == -1);
        }

        return hit;
    }

    /// <summary>
    /// Wirft einen <see cref="Ray"/> und gibt dessen <see cref="RaycastHit2D"/> zurück
    /// </summary>
    /// <param name="origin">Ursprung des <see cref="Ray"/>s</param>
    /// <param name="verticalDirection">Vertikale Richtung des ausgesendeten <see cref="Ray"/>s (-1 = unten, 1 = oben)</param>
    /// <param name="collisionLayers"><see cref="LayerMask"/>s, mit denen der <see cref="Ray"/> kollidieren kann</param>
    /// <param name="rayLength">Länge des ausgesendeten <see cref="Ray"/>s</param>
    /// <param name="velocity">Bewegungsrichtung, die in der Funktion abgeändert wird, wenn der ausgesendete <see cref="Ray"/> auf ein Objekt trifft</param>
    private RaycastHit2D CastRayVertically(Vector2 origin, float verticalDirection, LayerMask collisionLayers, float rayLength, ref Vector3 velocity)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.up * verticalDirection, rayLength, collisionLayers);
        Debug.DrawRay(origin, Vector2.up * verticalDirection * rayLength, Color.green);

        if (hit)
        {
            velocity.y = (hit.distance - colliderInset) * verticalDirection;
            rayLength = hit.distance; //TODO: benötigt?
            CollisionInfo.IsCollidingAbove = (verticalDirection == 1);
            CollisionInfo.IsCollidingBelow = (verticalDirection == -1);
        }

        return hit;
    }

    public abstract void Die(DeathCause cause, IEntity killer);
}

public class CollisionInfoEventArgs : EventArgs
{
    public CollisionInfoEventArgs(GameObject obj)
    {
        CollidingObject = obj;
    }

    public GameObject CollidingObject { get; private set; }
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