using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyBehaviour : MonoBehaviour {

    private bool canvasA = false;
    public Transform canvas;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R) && !canvasA)
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            TriggerBehaviors.time += Time.timeSinceLevelLoad;
            TriggerBehaviors.deathCount++;
        }
    }
}
