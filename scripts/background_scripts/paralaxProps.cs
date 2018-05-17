using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paralaxProps : MonoBehaviour {

    public float paralaxSpeed;
    private float playerSpeed;
    PlayerPropertyManeger ppm;
	// Use this for initialization
	void Start () {
        ppm = GameObject.Find("player").GetComponent<PlayerPropertyManeger>();
	}
	
	// Update is called once per frame
	void Update () {
        playerSpeed = ppm.speedX;

        transform.Translate((-1 * ppm.directionX * paralaxSpeed * Time.deltaTime), 0, 0);
	}
}
