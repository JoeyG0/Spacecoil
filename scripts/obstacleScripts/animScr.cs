using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animScr : MonoBehaviour {

    Animator anim;
    GameObject onThing;
    GravityOn go;
    // Use this for initialization
    void Start () {
        onThing = GameObject.Find("button");
        go = onThing.GetComponent<GravityOn>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (go.isOn == false) {
            anim.SetBool("isOn", false);

        }
        else {
            anim.SetBool("isOn", true);
        }
    }
}
