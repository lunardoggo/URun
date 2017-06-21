using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitOnClick : MonoBehaviour {

    public void QuitGame()
    {
        Debug.Log("Main-Menu: Button 'Spiel beenden' geklickt. Spiel beendet und verlassen.");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
