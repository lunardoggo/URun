using System.Collections;
using System;
using UnityEngine;

public class DissolvingPlatform : BasePlatform {

    [SerializeField, Range(0.1f, 25.0f)]
    private float dissolvementDelay = 5.0f;
    [SerializeField, Range(0.05f, 1.0f)]
    private float dissolvementSpeed = 1.0f;
    [SerializeField, Range(0.1f, 25.0f)]
    private float regenerationDelay = 5.0f;

    private event EventHandler<EventArgs> PlatformDissolved;

    private Coroutine dissolveRoutine = null;
    private Coroutine regenRoutine = null;
    private float dissolvementState = 1.0f;


    protected override void Start () {
        base.Start();
        PlatformDissolved += OnPlatformDissolved;

        dissolvementState = spriteRenderer.material.GetFloat("_DissAmo");
    }

    protected override void Update () {
        base.Update();

        UpdateRaycasts();
        CollisionInfo.Reset();
        DissolveOnAboveCollision();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        PlatformDissolved -= OnPlatformDissolved;
    }

    /// <summary>
    /// Prüft auf Collisions, wenn eine Collision auftritt, wird <see cref="DissolveLater"/> aufgerufen
    /// </summary>
    private void DissolveOnAboveCollision()
    {
        if (dissolveRoutine != null)
            return;

        Vector3 up = Vector3.up * RayLength;
        PlayerController player = null;
        if (GetFirstVerticalCollision(ref up, 1).TryGetComponent(out player))
        {
            dissolveRoutine = StartCoroutine(DissolveLater());
        }
    }

    /// <summary>
    /// Löst die Plattform nach Ablauf des <see cref="dissolvementDelay"/>s auf
    /// </summary>
    private IEnumerator DissolveLater()
    {
        yield return new WaitForSeconds(dissolvementDelay);
        do
        {
            Animate(true);
            yield return new WaitForEndOfFrame();
        } while (dissolvementState >= 0.0f);
        Animate(true);
        BoxCollider.enabled = false;

        if(PlatformDissolved != null)
        {
            PlatformDissolved.Invoke(this, new EventArgs());
        }
    }

    /// <summary>
    /// Regeneriert die Plattform nach Ablauf des eingestellten <see cref="regenerationDelay"/>
    /// </summary>
    private IEnumerator RegenLater()
    {
        this.StopCoroutine(ref dissolveRoutine);

        yield return new WaitForSeconds(regenerationDelay);
        do
        {
            Animate(false);
            yield return new WaitForEndOfFrame();
        } while (dissolvementState < 1.0f);
        dissolvementState = Mathf.Clamp01(dissolvementState);
        Animate(false);

        BoxCollider.enabled = true;

        regenRoutine = null;
    }

    /// <summary>
    /// Callback, wenn die Plattform vollständig aufgelöst wurde
    /// </summary>
    private void OnPlatformDissolved(object sender, EventArgs e)
    {
        this.StopCoroutine(ref dissolveRoutine);
        this.StopCoroutine(ref regenRoutine);
        regenRoutine = StartCoroutine(RegenLater());
    }

    /// <summary>
    /// Setzt den Wert für die Dissolvement-Shader-Animation auf dem Material der Plattform
    /// </summary>
    private void Animate(bool dissolve)
    {
        if (dissolvementState < 0.0f || dissolvementState > 1.0f)
        {
            dissolvementState = Mathf.Clamp01(dissolvementState);
        }
        else
        {
            dissolvementState += Time.deltaTime * dissolvementSpeed * (dissolve ? -1 : 1);
        }
        spriteRenderer.material.SetFloat("_DissAmo", dissolvementState);
    }
}
