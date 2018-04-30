using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathCOuntDisplayer : MonoBehaviour {

    private TextMeshProUGUI deathCount;

	// Use this for initialization
	void Start () {
        deathCount = GetComponent<TextMeshProUGUI>();
        deathCount.text = "Death Count: " + TriggerBehaviors.deathCount; 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
