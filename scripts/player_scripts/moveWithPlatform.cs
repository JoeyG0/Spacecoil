using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveWithPlatform : MonoBehaviour {
    float distanceToGround;
    RaycastHit2D hit;
    public bool hit1;
    // Use this for initialization
    void Start () {
        distanceToGround = GetComponent<BoxCollider2D>().bounds.extents.y;
	}

    private void Update() {

        hit = Physics2D.Raycast(transform.position, -Vector3.up, distanceToGround );
        Vector3 previousPlayerVelocity = GetComponent<Rigidbody2D>().velocity;

        if (hit.collider != null) {
            hit1 = true;
            if (hit.collider.GetComponent<moveUpAndDown>() != null) {
                GetComponent<Rigidbody2D>().AddForce(hit.collider.GetComponent<moveUpAndDown>().currentVelocity);
                //GetComponent<Rigidbody2D>().AddForce( new Vector2( 0, hit.collider.GetComponent<moveUpAndDown>().currentVelocity.y));
            }
            if (hit.collider.GetComponent<MoveLeftAndRight>() != null) {

                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, hit.collider.GetComponent<MoveLeftAndRight>().currentVelocity.x));
            }
        }
        else {
            hit1 = false;
        }
    }
}
