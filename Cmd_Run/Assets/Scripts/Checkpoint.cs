using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    private GameController gameController;

	void Start () {
        gameController = FindObjectOfType<GameController>();
	}
	
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Checkpoint erreicht");

            gameController.CurrentCheckpoint = gameObject;
        }
    }
}
