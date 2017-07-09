using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour {

    [SerializeField]
    private string sceneToLoad;
    [SerializeField, Range(0.001f, 0.25f)]
    private float fadeSpeed = 0.01f;

    private IItemController controller = null;
    private bool fadeOut = false;
    private float fadeProgress = 0.0f;
    private Texture2D fadeTexture = null;

    public void Start()
    {
        controller = GameObject.FindWithTag("GameController").GetComponent<IItemController>();
        fadeTexture = new Texture2D(1, 1);
        fadeTexture.SetPixel(0, 0, new Color(0f, 0f, 0f, 0f));
        fadeTexture.Apply();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(controller.HasCollectedMainCoin)
            {
                Debug.Log("next level");

                //GameTools.LoadNextLevel();
                fadeOut = true;
            }
            else
            {
                Debug.Log("Sammle 1 MainCoin ein");
            }
        }
    }

    private void LoadLevel()
    {
        SceneManager.LoadScene(sceneToLoad);
        //fortschritt speichern
        if(PlayerPrefs.GetInt(sceneToLoad.ToString()) ==0)
        {
            //level noch nicht aktiv -> freischalten
            PlayerPrefs.SetInt(sceneToLoad.ToString(), 1);
            PlayerPrefs.Save();
        }
        //scene Level_0X_M_1 -> PlayerPref -> 0 / 1
        Debug.Log("Level freigeschaltet? " + PlayerPrefs.GetInt(sceneToLoad.ToString()));
    }

    private void OnGUI()
    {
        if (fadeOut)
        {
            fadeTexture = new Texture2D(1, 1);
            fadeTexture.SetPixel(0, 0, new Color(0f, 0f, 0f, fadeProgress));
            fadeTexture.Apply();

            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);

            if (fadeProgress >= 1.0f)
                LoadLevel();
            fadeProgress += fadeSpeed;
        }
    }
}
