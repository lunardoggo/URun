using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using UnityEngine;
using System;

[RequireComponent(typeof(AudioSource))]
public class GameController : MonoBehaviour, IItemController, ILevelController, IMusicController {

    [SerializeField]
    private Text mainCoinText;
    [SerializeField]
    private Text healthText;
    [SerializeField]
    private Text coinsText;
    [SerializeField]
    private Text powerUpText;
    [SerializeField]
    private Checkpoint currentCheckpoint;
    [SerializeField]
    private List<PlatformSpawn> PlatformSpawns;
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private int health;
    [SerializeField]
    private TrackList tracks = null;

    public event EventHandler<CheckpointEventArgs> OnCheckpointChanged;

    public PlayerController Player { get; private set; }
    public bool HasCollectedMainCoin { get; private set; }
    public bool PlayerIsAlive { get { return health > 0; } }
    public Checkpoint CurrentCheckpoint
    {
        get { return currentCheckpoint; }
        set
        {
            if (value != null && currentCheckpoint != value)
            {
                currentCheckpoint = value;
                if (OnCheckpointChanged != null)
                    OnCheckpointChanged.Invoke(this, new CheckpointEventArgs(value));
            }
        }
    }

    private AudioSource musicSource = null;
    private float lastTimeScale = 1.0f;
    private PlayerStats statistics = null;

	private void Start () {
        statistics = CmdRun.PlayerStatistics;
        musicSource = GetComponent<AudioSource>();

        health = 3;
        player = GameObject.FindObjectOfType<PlayerController>();

        Player = player;

        UpdateMainCoinText();
        UpdateCoinText();
        UpdateHealthText();
    }
	
	private void Update () {
        if (!musicSource.isPlaying && tracks.Count > 0)
        {
            musicSource.clip = tracks.GetRandom();
            musicSource.Play();
        }
    }

    /// <summary>
    /// Aktualisiert das <see cref="powerUpText"/>-Label
    /// </summary>
    public void UsePowerUp(PowerUpItem item)
    {
        if (item != null)
            powerUpText.text = item.Duration + " s";
        else
            powerUpText.text = "0 s";
    }

    /// <summary>
    /// Fügt eine neue Münze zur Statistik hinzu
    /// </summary>
    /// <param name="item"></param>
    public void AddCoin(CoinItem item)
    {
        statistics.AddCoin(item);
        UpdateCoinText();
    }

    /// <summary>
    /// Fügt Gesundheit zum Spieler hinzu und aktualisiert das <see cref="healthText"/>-Label
    /// </summary>
    public void AddHealth()
    {
        health++;
        UpdateHealthText();
    }

    /// <summary>
    /// Setzt die Angabe, ob bereits mindestens 1 MainCoin eingesammelt wurde
    /// </summary>
    public void CollectedMainCoin(CoinMainItem item)
    {
        HasCollectedMainCoin = true;
        UpdateMainCoinText();
        statistics.AddMainCoin(item);
    }

    /// <summary>
    /// Wenn restartLevel == false, wird der Spieler zum letzten Checkpoint zurückgesetzt, sonst wird das Level neu gestartet
    /// </summary>
    public void RespawnPlayer(bool restartLevel)
    {
        health--;
        healthText.text = health.ToString();

        if (PlayerIsAlive)
        {
            player.transform.position = currentCheckpoint.transform.position;
            foreach (PlatformSpawn spawn in PlatformSpawns.Where(_spawn => _spawn != null))
            {
                spawn.SpawnPlatform();
            }
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    /// <summary>
    /// Spielt das nächste Lied in der <see cref="tracks"/> <see cref="TrackList"/> ab
    /// </summary>
    public void PlayNextTrack()
    {
        if (musicSource != null)
        {
            if (musicSource.isPlaying)
            {
                musicSource.Stop();
            }
            musicSource.clip = tracks.GetRandom();
            musicSource.Play();
        }
    }

    private void UpdateMainCoinText()
    {
        mainCoinText.text = statistics.PlayerMainCoins.ToString();
    }

    private void UpdateCoinText()
    {
        coinsText.text = statistics.PlayerCoins.ToString();
    }

    private void UpdateHealthText()
    {
        healthText.text = health.ToString();
    }
}
