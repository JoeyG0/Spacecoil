using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Momentum : MonoBehaviour {
     
    private Grappling_hook gh;
    Transform playerPosition;
    MoveLeftAndRight lr;
    moveUpAndDown ud;
    public bool hitUpDown;
    public bool hitLeftRight;
    Rigidbody2D prb;


    private void Start() {
       // playerPosition = GetComponent<Transform>();
       // gh = GetComponent<Grappling_hook>();
       // prb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {

        collision.transform.parent = gameObject.transform;

        
    }
    private void OnCollisionExit2D(Collision2D collision) {
        collision.transform.parent = null;


    }
}

