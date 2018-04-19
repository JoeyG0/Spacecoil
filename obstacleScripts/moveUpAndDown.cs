using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveUpAndDown : MonoBehaviour {
    //Imade sure that the leftBound would always be the first child and right bound would always be the second child

    public float speed = 5f;
    public float seconds = 0f;
    public bool wait = false;
    public bool hitUp = false;
    //start off going up
    public bool hitDown = true;

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.name == "upBound") {
            hitUp = true;
            hitDown = false;
            wait = true;

        }
        else if (collision.gameObject.name == "downBound") {
            //we have hit the right bound, now go left
            hitDown = true;
            hitUp = false;
            wait = true;
        }
    }

    private void Update() {

        //go left untill we hit the bounds
        if (hitUp == false && hitDown == true && wait == false) {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
        else if (hitUp == true && hitDown == false && wait == false) {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
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
