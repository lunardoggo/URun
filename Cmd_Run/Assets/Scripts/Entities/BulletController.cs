using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BulletController : MonoBehaviour, IProjectile {

    public IEntity Killer { get; private set; }
    public bool IsAlive { get; private set; }

    [SerializeField]
    private bool applyGravity = false;
    [SerializeField]
    private float gravity = -9.81f;
    [SerializeField]
    [Range(0.01f, 300.0f)]
    private float speed = 3.0f;
    [SerializeField]
    private LayerMask collisionLayers = 0;
    private float rayLength = 0.0f;
    private float raySpacing = 0.0f;
    private Coroutine destructionTimer = null;

    private const int rayCount = 3;
    private const float rayOffset = 0.015f;

    private void Start () {
        Killer = null;
        IsAlive = true;
        rayLength = GetComponent<BoxCollider2D>().bounds.size.x / 2 + rayOffset;
        raySpacing = GetComponent<BoxCollider2D>().bounds.size.y / rayCount;
        destructionTimer = StartCoroutine(DestructionTimer());
	}

    /// <summary>
    /// Aktualisiert die Position und prüft auf Kollisionen
    /// </summary>
	private void Update () {
        float currentSpace = -raySpacing;
        for (int i = 0; i < rayCount; i++)
        {
            Vector3 origin = transform.position;
            origin.y += currentSpace;
            currentSpace += raySpacing;
            RaycastHit2D hit = Physics2D.Raycast(origin, transform.right, rayLength, collisionLayers);
            if (hit)
            {
                IEntity entity = null;
                if (hit.collider.gameObject.TryGetComponent(out entity) && hit.collider.gameObject.CompareTag("Enemy"))
                {
                    entity.Die(DeathCause.EnemyTouched, this);
                }
                Destroy(this.gameObject);
                break;
            }
        }

        transform.position += transform.right * speed * Time.deltaTime;
    }

    private void OnDestroy()
    {
        this.StopCoroutine(ref destructionTimer);
    }

    /// <summary>
    /// Startet den Zerstörungstimer (20 Sekunden)
    /// </summary>
    private IEnumerator DestructionTimer()
    {
        yield return new WaitForSeconds(20.0f);
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Zerstört das übergeordnete <see cref="GameObject"/>
    /// </summary>
    public void Die(DeathCause cause, IEntity killer)
    {
        Destroy(this.gameObject);
    }
}
