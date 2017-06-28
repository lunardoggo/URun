using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour {

    public Text timerDisplay;
    [Range(15, ushort.MaxValue)]
    public ushort remainingSeconds = 200;

    private Coroutine timer = null;

	private void Awake () {
        timerDisplay.text = remainingSeconds.ToString();
        timer = StartCoroutine(RunTimer());
	}

    private void OnDestroy()
    {
        this.StopCoroutine(ref timer);
    }

    private IEnumerator RunTimer()
    {
        while (remainingSeconds > 0)
        {
            yield return new WaitForSeconds(1);
            remainingSeconds--;
            timerDisplay.text = remainingSeconds.ToString();
        }
        GameObject.FindWithTag("GameController").GetComponent<GameController>().RespawnPlayer(true);
    }
}
