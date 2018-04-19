using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityOn : MonoBehaviour {
    public GameObject reverse;
    public bool isOn = true;
   

	// Use this for initialization
	void Start () {
       
		Debug.Log(gameObject.tag);
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject.GetComponent<ButtonHit>().wasHit == true){
            reverse.tag = "Fall";
            isOn = false;
        }
        else
        {
            isOn = true;
            reverse.tag = "Reverse";
        }
	}
}
