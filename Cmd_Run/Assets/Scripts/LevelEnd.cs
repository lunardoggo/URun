using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour {

    public string sceneToLoad;

    private GameController controller = null;

    public void Start()
    {
        controller = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(controller.HasCollectedMainCoin)
            {
                Debug.Log("next level");

                //GameTools.LoadNextLevel();
                LoadLevel();
            }
            else
            {
                Debug.Log("Sammle 1 MainCoin ein");
            }
        }
    }
    void LoadLevel()
    {
        SceneManager.LoadScene(sceneToLoad);
        //fortschritt speichern
        if(PlayerPrefs.GetInt(sceneToLoad.ToString()) ==0)
        {
            //level noch nicht aktiv -> freischalten
            PlayerPrefs.SetInt(sceneToLoad.ToString(), 1);
        }
        //scene Level_0X_M_1 -> PlayerPref -> 0 / 1
        Debug.Log("Level freigeschaltet? " + PlayerPrefs.GetInt(sceneToLoad.ToString()));
    }
}
