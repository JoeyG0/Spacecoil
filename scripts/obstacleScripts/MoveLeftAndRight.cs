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
    public bool goingLeft;
    public bool goingRight;
    public bool justHit;
    public Vector3 currentVelocity {
        get {
            return _currentVelocity;
        }
    }

    private Vector3 _currentVelocity;
    private Vector3 lastPos;


    private void Start() {

        lastPos = transform.position;
    }

   /* private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name == "player" && !gh.enabled) {
            collision.transform.parent = gameObject.transform;
        }

        // collision.transform.parent = gameObject.transform;

    }
    private void OnCollisionExit2D(Collision2D collision) {
        collision.transform.parent = null;

    }*/

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "leftBound") {
            justHit = true;
            hitLeft = true;
            hitRight = false;
            wait = true;

        }
        else if (collision.gameObject.name == "rightBound") {
            //we have hit the right bound, now go left
            justHit = true;
            hitRight = true;
            hitLeft = false;
            wait = true;
        }
    }

    private void Update() {
        if (hitLeft) {           
            goingRight = true;
            goingLeft = false;
        }
        else {
            goingLeft = true;
            goingRight = false;
        }
        //go left untill we hit the bounds
        if(hitLeft == false && hitRight == true && wait == false) {
            /*if (justHit) {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                justHit = false;
            }
            //GetComponent<Rigidbody2D>().AddForce(Vector3.left * (speed * Time.deltaTime));*/
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        else if (hitLeft == true && hitRight == false && wait == false) {
          /*  if (justHit) {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                justHit = false;
            }
           // GetComponent<Rigidbody2D>().AddForce(Vector3.right * (speed * Time.deltaTime));*/
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        else {
            StartCoroutine("waitXSeconds");
        }
        _currentVelocity = (transform.position - lastPos)/Time.deltaTime;
        lastPos = transform.position;
       // Debug.Log(_currentVelocity);
    }

    //waits the ammount of seconds specified
    IEnumerator waitXSeconds() {
        yield return new WaitForSeconds(seconds);
        wait = false;
    }
    // Update is called once per frame

}
