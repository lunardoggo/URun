using System.Collections;
using UnityEngine;
using System;

[RequireComponent(typeof(BoxCollider2D))]
public class PowerUpItem : MonoBehaviour, ICollectible {

    public event EventHandler<EventArgs> OnPowerUpExpired;
    public PowerUpKind Kind { get { return kind; } }
    public ushort Duration { get { return powerUpDuration; } }

    [SerializeField]
    private PowerUpKind kind = PowerUpKind.Bullet;
    [SerializeField]
    [Range(1, ushort.MaxValue)]
    private ushort powerUpDuration = 10;
    private IItemController controller = null;
    private Coroutine timer = null;

    public void Start()
    {
        controller = GameObject.FindWithTag("GameController").GetComponent<IItemController>();
    }

    public void Cancel()
    {
        this.StopCoroutine(ref timer);
    }

    public void Activate()
    {
        if (timer == null)
        {
            timer = StartCoroutine(StartTimer());
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            controller.Player.SetPowerUp(this);
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
    }

    private IEnumerator StartTimer()
    {
        while (powerUpDuration > 0)
        {
            yield return new WaitForSeconds(1.0f);
            powerUpDuration--;
        }
        if (OnPowerUpExpired != null)
        {
            OnPowerUpExpired.Invoke(this, new EventArgs());
            Destroy(this.gameObject);
        }
    }
}

public enum PowerUpKind : byte
{
    Bullet = 0
}