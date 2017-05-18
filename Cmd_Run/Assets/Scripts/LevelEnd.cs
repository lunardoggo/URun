using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour {

    private PlayerStats statistics;

    public void Start()
    {
        statistics = PlayerStats.Instance;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(statistics.PlayerMainCoins > 1)
            {
                Debug.Log("next level");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                Debug.Log("Sammle 1 MainCoin ein");
            }
           
        }
    }
}
