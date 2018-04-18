using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunRenderBehavior : MonoBehaviour {
    public Vector2 mousePosition = new Vector2();
    public Transform gun;
    public Vector2 gunPosition = new Vector2();
    public Vector2 mouseDir = new Vector2();
    public GameObject player;
    private Transform playerTransform;
    private SpriteRenderer playerFlip;
    private SpriteRenderer flipGun;
    public float gunAngle;
    Camera Cam;
    Animator anim;
    PlayerMove cl;
    public bool boosting;


    // Use this for initialization
    void Start () {
        cl = player.GetComponent<PlayerMove>();
        anim = GetComponent<Animator>();
        playerTransform = player.GetComponent<Transform>();
        gun = GetComponent<Transform>();
        playerFlip = player.GetComponent<SpriteRenderer>();
        flipGun = GetComponent<SpriteRenderer>();
        Cam = Camera.main;
    }

    private void FixedUpdate() {


        boosting = cl.boosting;
        anim.SetBool("boosting", boosting);

        mousePosition = Cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        gunPosition = gun.position;
        mouseDir.x = mousePosition.x - gunPosition.x;
        mouseDir.y = mousePosition.y - gunPosition.y;
        if (mousePosition.x < playerTransform.position.x && (playerTransform.rotation.z * Mathf.Rad2Deg) < 40) {
            playerFlip.flipX = true;
            flipGun.flipY = true;
        }
        else if (mousePosition.x > playerTransform.position.x && (playerTransform.rotation.z * Mathf.Rad2Deg) < 40){
            playerFlip.flipX = false;
            flipGun.flipY = false;
        }
       /* else if ((playerTransform.rotation.z * Mathf.Rad2Deg) >= 40 && mousePosition.y > playerTransform.position.y && mousePosition.x < playerTransform.position.x) {
            
            playerFlip.flipX = false;
            flipGun.flipY = false;
        }
        else if ((playerTransform.rotation.z * Mathf.Rad2Deg) >= 40 && mousePosition.y < playerTransform.position.y && mousePosition.x > playerTransform.position.x) {
            playerFlip.flipX = true;
            flipGun.flipY = true;
        }*/


        gunAngle = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg; //getting the angle and turning it from radians to degrees
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, gunAngle));

    }
}
