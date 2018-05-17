using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour {

    public float speed = 50;
    Quaternion angle;
    Rigidbody2D rb2d;
    public bool hit = false;
    DestroyBullet destroy;

    private void Start() {

        angle = transform.rotation;
        rb2d = GetComponent<Rigidbody2D>();
        destroy = GetComponent<DestroyBullet>();

    }

    void Update () {

        if (!hit) {
            //moves the bullet in the direction it was instantiated in 
            transform.Translate(speed * Time.deltaTime, 0, 0);

        }

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.tag != "Player") {
            hit = true;
            if (collision.collider.gameObject.tag == "Button") {

                collision.collider.GetComponent<ButtonHit>().wasHit = !collision.collider.GetComponent<ButtonHit>().wasHit;
            }
            destroy.deactivate();
        }

    }

    


}
