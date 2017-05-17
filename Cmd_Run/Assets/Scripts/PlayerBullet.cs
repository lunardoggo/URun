using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {


    public Transform bulletSpawn;
    public Transform player;
    public bool PowerUp = false;

   // public SpriteRenderer sr;

    public Rigidbody2D bulletPrefab;

    Rigidbody2D clone;

    public float bulletSpeed = 300;

	// Use this for initialization
	void Start () {
        bulletSpawn = GameObject.Find("BulletSpawn").transform;
        
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1"))
        {
            if (PowerUp)
            {
                // Feuer frei! -> Attack()
                Debug.Log("Feuer frei!");
                Attack();
            }
        }
	}

    void Attack()
    {
        if(bulletSpawn.position.x > player.position.x)
        {
            clone = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            clone.AddForce(bulletSpawn.transform.right * bulletSpeed);
        }
        else
        {
            //sr.flipX = true;
            clone = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            clone.AddForce(bulletSpawn.transform.right * -bulletSpeed);
        }
        
    }
}
