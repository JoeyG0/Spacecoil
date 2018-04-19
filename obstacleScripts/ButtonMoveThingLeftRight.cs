using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMoveThingLeftRight : MonoBehaviour {

    public GameObject mover;
    public float speed1;
    public float seconds1;
    public float speed2;
    public float seconds2;
    MoveLeftAndRight k;

    // Use this for initialization
    void Start() {
        k = mover.GetComponent<MoveLeftAndRight>();

    }

    // Update is called once per frame
    void Update() {
        if (gameObject.GetComponent<ButtonHit>().wasHit == true) {
            k.speed = speed1;
            k.seconds = seconds1;
        }
        else {
            k.speed = speed2;
            k.seconds = seconds2;
        }



    }
}

