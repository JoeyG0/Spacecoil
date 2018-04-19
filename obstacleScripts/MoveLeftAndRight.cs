using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftAndRight : MonoBehaviour {

    //Imade sure that the leftBound would always be the first child and right bound would always be the second child
    
    public float speed = 5f;
    public float seconds = 0f;
    private bool wait = false;
    public bool hitLeft = false;
    //start off going left
    public bool hitRight = true;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "leftBound") {
            hitLeft = true;
            hitRight = false;
            wait = true;

        }
        else if (collision.gameObject.name == "rightBound") {
            //we have hit the right bound, now go left
            hitRight = true;
            hitLeft = false;
            wait = true;
        }
    }

    private void Update() {

        //go left untill we hit the bounds
        if(hitLeft == false && hitRight == true && wait == false) {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        else if (hitLeft == true && hitRight == false && wait == false) {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        else {
            StartCoroutine("waitXSeconds");
        }
    }

    //waits the ammount of seconds specified
    IEnumerator waitXSeconds() {
        yield return new WaitForSeconds(seconds);
        wait = false;
    }
    // Update is called once per frame

}
