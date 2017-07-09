using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class Checkpoint : MonoBehaviour {

    private ILevelController gameController;
    private SpriteRenderer checkpointSprite;

    private void OnDestroy()
    {
        if (gameController != null)
            gameController.OnCheckpointChanged -= OnCheckpointChanged;
    }

    void Start () {
        gameController = GameObject.FindWithTag("GameController").GetComponent<ILevelController>();
        if (gameController != null)
            gameController.OnCheckpointChanged += OnCheckpointChanged;

        checkpointSprite = transform.GetComponentInChildren<SpriteRenderer>();
	}
	
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Checkpoint erreicht");

            gameController.CurrentCheckpoint = this;
        }
    }

    private void OnCheckpointChanged(object sender, CheckpointEventArgs e)
    {
        if(checkpointSprite != null)
        {
            checkpointSprite.material.SetFloat("_Substitute", e.Checkpoint != this ? 0.0f : 1.0f);
        }
    }
}


public class CheckpointEventArgs : EventArgs
{
    public CheckpointEventArgs(Checkpoint checkpoint)
    {
        Checkpoint = checkpoint;
    }

    public Checkpoint Checkpoint { get; private set; }
}