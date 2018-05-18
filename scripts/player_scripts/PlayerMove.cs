using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;
using System;
using Debug = UnityEngine.Debug;


public class PlayerMove : MonoBehaviour {

    public float fireRateSec;
    public float shake = 3f;
    public float shakeTime = .2f;
    CameraFollow camMove;
    GameObject gun;
    gunRenderBehavior gunRender;
    public GameObject Gunbolt;
    Grappling_hook gh;
    float angle;
    public int directionX = 0;
    public float speedX = 0f;
    public int directionY = 0;
    public float speedY = 0f;
    bool overheat = false;
    public bool clicked = false;
    public int shootCount = 0;
    public float Force = 1200.0f;
    public float maxSpeed = 50.0f;
    public bool boosting;
    public bool isGrounded = false;
    public Vector2 mouseDir = new Vector2();
    private GameObject bulletSpawn;


    private Vector2 mousePosition = new Vector2();
    private Rigidbody2D rb2D;
    private Vector2 playerPosition = new Vector2();
     
    private GameObject button;
    private bool canShoot = true;

    public float thrustForce = 50f;
    
    Camera Cam;
    public double threshhold = 200;
    public float timeToClick = 450;
    public int overheatLevel = 2;
    public double heatLevel;

    // Use this for initialization
    void Start() {

        bulletSpawn = GameObject.Find("bulletSpawn");
        gun = GameObject.Find("gunRender");
        gunRender = gun.GetComponent<gunRenderBehavior>();
        heatLevel = 1;
        gh = GetComponent<Grappling_hook>();
        rb2D = GetComponent<Rigidbody2D>();
        Cam = Camera.main;
        camMove = Cam.GetComponent<CameraFollow>();
    }

    // Update is called once per frame
    void Update() {
        bool pauseState = GameObject.Find("Pause Menu").GetComponent<PauseMenu>().isPaused;
        
        if (Input.GetMouseButtonDown(0) && !pauseState) {

            gunRender.shootAnimationController(true, false);
            shoot();
            gunRender.shootAnimationController(false,true);
        }
    }


    IEnumerator waitXSeconds() {
        yield return new WaitForSeconds(fireRateSec);
        canShoot = true;
    }

    void shoot() {

        if (canShoot) {

        
            angle = gunRender.gunAngle;

            //find an inactive bolt in our list of pooled bolts
            GameObject bolt = ObjectPooler.current.GetObject();
            if( bolt == null) {
                return;
            }

            //set the bolt's rotation equal to that of the gun, and spawn it where we placed the empty game object that is childed to the gun
            bolt.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            bolt.transform.position = new Vector2(bulletSpawn.transform.position.x, bulletSpawn.transform.position.y);
            //set the bolt to be active
            bolt.SetActive(true);

            shootCount++;

            Effect();

            

            //overheating code
            if (heatLevel < threshhold) {

                heatLevel += overheatLevel;
                rb2D.AddForce((-1 * mouseDir) * (int)(Force * (8 / heatLevel) * 1.5));

            }
            else {
              rb2D.AddForce(-1 * mouseDir * 12);
            }



            //handles the movement of the player
            Vector2 temp;
            if (Math.Abs(rb2D.velocity.x) > maxSpeed && Math.Abs(rb2D.velocity.y) > maxSpeed) {

                temp = new Vector2(rb2D.velocity.x * 0.85f, rb2D.velocity.y * 0.85f);
                rb2D.velocity = temp;
            }
            else if (Math.Abs(rb2D.velocity.x) > maxSpeed) {

                temp = new Vector2(rb2D.velocity.x * 0.85f, rb2D.velocity.y);
                rb2D.velocity = temp;
            }
            else if (Math.Abs(rb2D.velocity.y) > maxSpeed) {

                temp = new Vector2(rb2D.velocity.x, rb2D.velocity.y * 0.85f);
                rb2D.velocity = temp;
            }
            //sets the boolean that checks if we can shoot to be false, unless the seconds that we have to wait between shooting is zero.
            //in that case there is no point going into the subroutine
            if (fireRateSec != 0) {
                canShoot = false;
            }
        }
        else {
            //goes into the Coroutine, to wait the specified amount of seconds before we can shoot again
            StartCoroutine("waitXSeconds");
        }

    }

    public void thrust() {

        if (Input.GetMouseButton(1) && isGrounded == true || Input.GetMouseButton(1) && gh.isEnabled == true) {
            boosting = true;
            rb2D.AddForce((-1 * mouseDir) * thrustForce);

        }
        else {
            boosting = false;
        }
    }
    private void Effect() {

        if (heatLevel < threshhold) {
            camMove.shakeCamera(shake, shakeTime);
            heatLevel += overheatLevel;
        }



    }

    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "movingFloor") {

            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {

        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "movingFloor") {

            isGrounded = false;
        }
    }

    private void FixedUpdate() {
        thrust();
        if (heatLevel > 1 && gh.isEnabled != true) {
            heatLevel -= 0.1;
        }
        if (heatLevel < 1)
            heatLevel = 1;
        if (isGrounded == true) {
            if (heatLevel > 1) {
                heatLevel = 1;
            }
            if (heatLevel < 1)
                heatLevel = 1;
        }
        speedX = rb2D.velocity.x;
        speedY = rb2D.velocity.y;
        if (speedX < 0) {

            directionX = -1;
            //we're changing the direction of the player sprite
            /*if(flip.flipX != true) {
                GetComponent<SpriteRenderer>().flipX = true;
            }*/
        }
        else if (speedX > 0) {
            /* if (flip.flipX != false) {
                 GetComponent<SpriteRenderer>().flipX = false;
             }*/
            directionX = 1;
        }
        else {
            directionX = 0;
        }
        if (speedY < 0) {
            directionY = -1;
        }
        else if (speedY > 0) {
            directionY = 1;
        }
        else {
            directionY = 0;
        }



        mousePosition = Cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        playerPosition = rb2D.position;
        mouseDir = mousePosition - playerPosition;
        mouseDir = mouseDir.normalized;


    }

}
