using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour {

    [Range(0.0f, 50.0f)]
    public float smoothTime = 0.3f;
    public GameObject followObject;
    public bool canGoLeft = true;
    public Vector3 followOffset = Vector3.zero;
    
    private Vector3 currentVelocity = Vector3.zero;
    private const float offsetFactor = 0.1f;
    
	void Start () {
		
	}
	
	void Update () {

        Vector3 newPosition = (Vector3.SmoothDamp(transform.position, followObject.transform.position, ref currentVelocity, smoothTime) + Vector3.back) + followOffset * offsetFactor;

        if (!canGoLeft && newPosition.x < transform.position.x) 
        {
            newPosition.x = transform.position.x;
        }

        transform.position = newPosition;
	}
}
