using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartOnClick : MonoBehaviour {

    public string sceneName;

	public void StartLevel()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(sceneName);
        Debug.Log("Main-Menu: Klick auf Button 'Spiel starten'. Level 1 geladen.");
    }
}
