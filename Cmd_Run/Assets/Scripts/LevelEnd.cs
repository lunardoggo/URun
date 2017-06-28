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

                GameTools.LoadNextLevel();
            }
            else
            {
                Debug.Log("Sammle 1 MainCoin ein");
            }
        }
    }
}
