using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawn : MonoBehaviour {

    /// <summary>
    /// Speichert, ob die Platform zerstört ist, um sie später erneut zu spawnen
    /// </summary>
    [SerializeField]
    private bool platformDestroyed = false;
    [SerializeField]
    private BasePlatform platformPrefab = null;
    private BasePlatform clone = null;

    // Wenn Spieler stirbt sollen platformen spawnen wo keine platformen sind:
    // Funktion die prüft ob objekt zerstört ist 
    // im spawn skript diese funktion aufrufen und wenn objekt zerstört neue platform spawnen

    void Start () {
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
            clone.OnPlatformDestroyed -= OnPlatformDestroyed;
            Destroy(clone.gameObject);
        }
        clone = Instantiate(platformPrefab, transform.position, transform.rotation);
        clone.OnPlatformDestroyed += OnPlatformDestroyed;
    }

    /// <summary>
    /// Wird bei der Zerstörung der geklonten <see cref="BasePlatform"/> aufgerufen
    /// </summary>
    private void OnPlatformDestroyed(object sender, EventArgs e)
    {
        platformDestroyed = true;
        if (sender.GetType().Equals(typeof(BasePlatform)))
        {
            ((BasePlatform)sender).OnPlatformDestroyed -= OnPlatformDestroyed;
        }
    }
}
