using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    private bool isPause = false;
    private Rect butRect;
    private float ctrlWidth = 160;
    private float ctrlHeight = 30;
	// Use this for initialization
	void Awake () {
        butRect = new Rect((Screen.width-ctrlWidth)/2, 0, ctrlWidth, ctrlHeight);
	}

    void OnGUI()
    {
        if (isPause)
        {
            butRect.y = (Screen.height - ctrlHeight) / 4;
            if (GUI.Button(butRect, "Weiter"))
            {
                ToggleTimeScale();
                Debug.Log("Sub-Menu: Button 'Weiter' geklickt. Spiel fortgesetzt");
            }
            butRect.y += ctrlHeight + 20;
            if (GUI.Button(butRect, "Hauptmenü"))
            {
                ToggleTimeScale();
                SceneManager.LoadScene("MainMenu");
                Debug.Log("Sub-Menu: Button 'MainMenu' geklickt. Wechsel in Hauptmenü");
            }
            butRect.y += ctrlHeight + 20;
            if(GUI.Button(butRect, "Level-Auswahl"))
            {
                SceneManager.LoadScene("Levelselect");
                ToggleTimeScale();
                Debug.Log("Sub-Menu: Button 'Level-Auswahl' geklickt, neues Level gewählt - Akutell noch in Bearbeitung");
            }
            butRect.y += ctrlHeight + 20;
            if(GUI.Button(butRect, "Spiel beenden"))
            {
                ToggleTimeScale();
                Debug.Log("Sub-Menu: Button 'Spiel beenden' geklickt. Spiel beenden");
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                #else
                Application.Quit();
                #endif
            }
        }
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            ToggleTimeScale();
		
	}
    void ToggleTimeScale()
    {
        if (!isPause)
        {
            Time.timeScale = 0;
        }
        else { Time.timeScale = 1; }
        isPause = !isPause;
    }
}
