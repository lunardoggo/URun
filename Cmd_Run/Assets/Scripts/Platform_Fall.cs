using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Fall : MonoBehaviour {

    private Rigidbody2D rb2dPlatform;
    public float delay;
    private GameObject platform;
    public PlatformManager platformManager;

	// Use this for initialization
	void Start () {
        platformManager = FindObjectOfType<PlatformManager>();
        rb2dPlatform = GetComponent<Rigidbody2D>();
        platform = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.tag == "Player")
        {
            Debug.Log("Platform Fällt");
            StartCoroutine(Fall());
            
            
        }
    }
    IEnumerator Fall()
    {
        yield return new WaitForSeconds(delay);
        rb2dPlatform.isKinematic = false;
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
        platformManager.PlatformDestroyed = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Killzone")
        {
           
            Destroy(platform);
            platformManager.PlatformDestroyed = true;

        }
    }
}
