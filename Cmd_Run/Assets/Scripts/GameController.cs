using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Text healthText;
    public Text coinsText;
    public Text powerUpText;

    public GameObject currentCheckpoint;
    public PlatformManager platformManager;
    public PlayerController player;

    public int health;

    public bool IsPaused { get; private set; }
    public bool PlayerIsAlive { get { return health > 0; } }

    private float lastTimeScale = 1.0f;
    private PlayerStats statistics;

	private void Start () {
        statistics = PlayerStats.Instance;
        health = 3;
        player = GameObject.FindObjectOfType<PlayerController>();

        UpdateCoinText();
    }
	
	private void Update () {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SetPaused(!IsPaused);
        }
	}

    public void UpdateCoinText()
    {
        coinsText.text = statistics.PlayerCoins.ToString();
    }

    public void SetPaused(bool paused)
    {
        if (paused != IsPaused)
        {
            IsPaused = paused;
            if (paused)
            {
                lastTimeScale = Time.timeScale;
                Time.timeScale = 0;
                //TODO: Pausen-UI einblenden
            }
            else
            {
                Time.timeScale = lastTimeScale;
                //TODO: Pausen-UI ausblenden
            }
        }
    }

    public void RespawnPlayer(bool restartLevel)
    {
        health--;
        healthText.text = health.ToString();

        if (PlayerIsAlive)
        {
            player.transform.position = currentCheckpoint.transform.position;
            platformManager.SpawnPlatform();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //TODO: Lose-Screen
        }
    }
}
