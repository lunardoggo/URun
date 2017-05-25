using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawn : MonoBehaviour {

    [SerializeField]
    private BasePlatform platformPrefab = null;
    private BasePlatform clone = null;

    /// <summary>
    /// Gibt zurück, ob die untergeordnete <see cref="BasePlatform"/> bereits zerstört wurde oder nicht
    /// </summary>
    public bool IsDestroyed { get; private set; }

    // Wenn Spieler stirbt sollen platformen spawnen wo keine platformen sind:
    // Funktion die prüft ob objekt zerstört ist 
    // im spawn skript diese funktion aufrufen und wenn objekt zerstört neue platform spawnen

    private void Start () {
        CreateClone();
    }

    /// <summary>
    /// Spawnt eine neue <see cref="BasePlatform"/>
    /// </summary>
    public void SpawnPlatform()
    {
        CreateClone();
    }

    /// <summary>
    /// Klont die <see cref="BasePlatform"/> "platformPrefab" an der Position dieses <see cref="GameObject"/>s und zerstört den alten Klon, falls dieser existiert
    /// </summary>
    private void CreateClone()
    {
        if(clone != null)
        {
            Destroy(clone.gameObject);
        }
        clone = Instantiate(platformPrefab, transform.position, transform.rotation);
        if(clone.GetType().Equals(typeof(FallingPlatform)))
        {
            ((FallingPlatform)clone).OnPlatfromFalling += OnPlatformFalling;
        }
        clone.OnPlatformDestroyed += OnPlatformDestroyed;
        IsDestroyed = false;
    }

    /// <summary>
    /// Wird bei der Zerstörung der geklonten <see cref="BasePlatform"/> aufgerufen
    /// </summary>
    private void OnPlatformDestroyed(object sender, EventArgs e)
    {
        IsDestroyed = true;
        if (sender.GetType().Equals(typeof(BasePlatform)))
        {
            ((BasePlatform)sender).OnPlatformDestroyed -= OnPlatformDestroyed;
        }
    }

    /// <summary>
    /// Wird beim Fallen der geklonten <see cref="BasePlatform"/>/>, wenn sie dem Typ <see cref="FallingPlatform"/> entspricht, aufgerufen
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnPlatformFalling(object sender, EventArgs e)
    {
        if(sender.GetType().Equals(typeof(FallingPlatform)))
        {
            IsDestroyed = true;
            ((FallingPlatform)sender).OnPlatfromFalling -= OnPlatformFalling;
            ((FallingPlatform)sender).OnPlatformDestroyed -= OnPlatformDestroyed;
        }
    }
}
