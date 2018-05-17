using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeCountDisplay : MonoBehaviour {
    private TextMeshProUGUI deathCount;
	// Use this for initialization
	void Start () {
        deathCount = GetComponent<TextMeshProUGUI>();
        deathCount.text = "Time to Complete: " + System.Math.Round(PlayerPropertyManeger.time/60,2)+" Mins"; 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
