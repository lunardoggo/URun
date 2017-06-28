using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsController : MonoBehaviour {

    [SerializeField]
    private float secondsToWait = 5.0f;

    private Coroutine endTimerRoutine;

	private void Awake () {
        endTimerRoutine = StartCoroutine(EndTimer());
	}

    private void OnDestroy()
    {
        this.StopCoroutine(ref endTimerRoutine);
    }

    private void Update () {
		if(Input.GetButtonDown("Fire1"))
        {
            GameTools.LoadNextLevel();
        }
	}

    private IEnumerator EndTimer()
    {
        yield return new WaitForSeconds(secondsToWait);
        GameTools.LoadNextLevel();
    }
}
