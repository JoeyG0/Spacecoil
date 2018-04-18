using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrust : MonoBehaviour {
    
    Vector2 mouseDirection {
        get {
            return pl.mouseDir;
        }
    }
    PlayerMove pl;
	// Use this for initialization
	void Start () {
        pl = GetComponent<PlayerMove>();
	}
	
    // Update is called once per frame
    void Update () {
        Debug.Log(mouseDirection.x);
	}
}
