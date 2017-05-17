using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_move_y : MonoBehaviour {

    public float speed;
    private Vector3 startPos, newPos;
    // Use this for initialization
    void Start () {
        startPos = transform.position;
        speed = Random.Range(3f, 6f);
    }
	
	// Update is called once per frame
	void Update () {
        newPos = startPos;
        newPos.y = newPos.y + Mathf.PingPong(Time.time * speed, 10) - 5;
        transform.position = newPos;
    }
}
