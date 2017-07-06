using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour {

    public string sceneToLoad;
	// Use this for initialization
	void Start () {
        PlayerPrefs.SetInt("Level_01", 1);
		if(PlayerPrefs.GetInt(sceneToLoad.ToString())==1)
        {
            //Level aktiv -> Button aktivieren
            this.GetComponent<Button>().interactable = true;
        }

        else
        {
            //Level inaktiv -> Button deaktivieren
            this.GetComponent<Button>().interactable = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadLevel() {
        SceneManager.LoadScene(sceneToLoad);
    }
}
