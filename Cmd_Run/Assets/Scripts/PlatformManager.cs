using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour {

    // Spawn Punkt erstellen an welchem dieses Skript hängt
    // Beim Start wird ein Fall_Platform Objekt erstellt
    private Transform PlatformSpawn;
    public Rigidbody2D PlatformPrefab;
    Rigidbody2D clone;
    // Merke wenn die Platform zerstört ist durch auslösen in Platform_Fall skript bei der zerstörung
    public bool PlatformDestroyed = false;
    // Wenn Spieler stirbt sollen platformen spawnen wo keine platformen sind:
    // Funktion die prüft ob objekt zerstört ist 
    // im spawn skript diese funktion aufrufen und wenn objekt zerstört neue platform spawnen

    // Use this for initialization
    void Start () {
        PlatformSpawn = gameObject.transform;
        clone = Instantiate(PlatformPrefab, PlatformSpawn.position, PlatformSpawn.rotation);
    }

    public void SpawnPlatform()
    {
        Debug.Log("Platform Spawnn");
        if(PlatformDestroyed)
        {
            Debug.Log("Platform ist gespawnt");
            clone = Instantiate(PlatformPrefab, PlatformSpawn.position, PlatformSpawn.rotation);
        }
        
    }
	
	
}
