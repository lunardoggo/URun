using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour {

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
                Scene activeScene = SceneManager.GetActiveScene();

                //Wenn buildindex der aktuellen Szene kleiner der Szenenanzahl ist (noch Szenen zum Laden vorhanden sind) -> Lade die nächste, sonst 0
                if (SceneManager.sceneCount - 1 > activeScene.buildIndex)
                {
                    SceneManager.LoadScene(activeScene.buildIndex + 1);
                }
                else
                {
                    SceneManager.LoadScene(0);
                }
                SceneManager.UnloadSceneAsync(activeScene.buildIndex);
            }
            else
            {
                Debug.Log("Sammle 1 MainCoin ein");
            }
        }
    }
}
