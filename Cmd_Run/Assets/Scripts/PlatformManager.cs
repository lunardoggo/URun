using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour {

    /// <summary>
    /// Speichert, ob die Platform zerstört ist, um sie später erneut zu spawnen
    /// </summary>
    [SerializeField]
    private bool platformDestroyed = false;
    [SerializeField]
    private BasePlatform platformPrefab;
    
    private BasePlatform clone;

    // Wenn Spieler stirbt sollen platformen spawnen wo keine platformen sind:
    // Funktion die prüft ob objekt zerstört ist 
    // im spawn skript diese funktion aufrufen und wenn objekt zerstört neue platform spawnen

    void Start () {
        CreateClone();
    }

    public void SpawnPlatform()
    {
        Debug.Log("Platform spawnt = " + platformDestroyed);
        if(platformDestroyed)
        {
            Debug.Log("Platform ist gespawnt");
            CreateClone();
        }
        
    }

    private void CreateClone()
    {
        if(clone != null)
        {
            clone.OnPlatformDestroyed -= OnPlatformDestroyed;
            Destroy(clone);
        }
        clone = Instantiate(platformPrefab, transform.position, transform.rotation);
        clone.OnPlatformDestroyed += OnPlatformDestroyed;
    }

    private void OnPlatformDestroyed(object sender, EventArgs e)
    {
        platformDestroyed = true;
        if (sender.GetType().Equals(typeof(BasePlatform)))
        {
            ((BasePlatform)sender).OnPlatformDestroyed -= OnPlatformDestroyed;
        }
    }
}
